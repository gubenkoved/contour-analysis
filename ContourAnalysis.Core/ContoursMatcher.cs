using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourAnalysis.Core
{
    public class ContoursMatcher
    {
        public class Match
        {
            public Contour SourceContour { get; set; }
            public ContourInformation FamiliarContour { get; set; }
            public Complex MaxNSP { get; set; }
            public Complex ACF_NSP { get; set; }
            public bool ReverseUsed { get; set; }            
        }

        private float m_minNsp;
        public float MinNSP
        {
            get { return m_minNsp; }
            set { m_minNsp = value; }
        }

        private float m_minACF_NSP;
        public float MinACF_NSP
        {
            get { return m_minACF_NSP; }
            set { m_minACF_NSP = value; }
        }
        
        private bool m_checkBothDirections;
        public bool CheckBothDirections
        {
            get { return m_checkBothDirections; }
            set { m_checkBothDirections = value; }
        }

        private IEnumerable<ContourInformation> m_familiarContours;
        public IEnumerable<ContourInformation> FamiliarContours
        {
            get { return m_familiarContours; }
            set 
            {
                m_familiarContours = value;
            }
        }

        public ContoursMatcher(float minNsp = 0.8f, float minAcfNsp = 0.7f, int targetLen = 30)
        {
            m_minNsp = minNsp;
            m_minACF_NSP = minAcfNsp;
        }

        /// <remarks>
        /// Best performance achieved when familiar contour len are equal between it and with specified contour
        /// </remarks>
        public Match GetMatch(Contour contour, bool fillComarativeInfo = false)
        {
            if (m_familiarContours == null)
                return null;

            Match bestMatch = null;

            foreach (var familiar in m_familiarContours)
            {
                ComplexSequence bringed = contour;

                if (contour.Points.Length != familiar.Contour.Points.Length)
                    bringed = contour.BringLength(familiar.Contour.Points.Length);

                Match currentMatch = GetPotentialMatch(familiar, bringed);

                if (m_checkBothDirections)
                {
                    Match currentMatchRevesed = GetPotentialMatch(familiar, bringed.Reverse());

                    if (currentMatchRevesed != null)
                    {
                        if (currentMatch == null ||
                        currentMatchRevesed.MaxNSP.AbsoluteValue() > currentMatch.MaxNSP.AbsoluteValue())
                        {
                            currentMatch = currentMatchRevesed;
                            currentMatch.ReverseUsed = true;
                        }
                    }
                }

                if (currentMatch != null)
                {
                    if (bestMatch == null || currentMatch.MaxNSP.AbsoluteValue() > bestMatch.MaxNSP.AbsoluteValue())
                    {
                        currentMatch.SourceContour = contour;
                        bestMatch = currentMatch;
                    }

                    if (fillComarativeInfo)
                    {
                        familiar.MaxNSP = currentMatch.MaxNSP;
                        familiar.ReverseUsed = currentMatch.ReverseUsed;
                        familiar.ACF_NSP = currentMatch.ACF_NSP;
                    }
                }

            }

            return bestMatch;
        }
        /// <remarks>
        /// Returns all match for contour collection with using multi-threading
        /// </remarks>
        public IEnumerable<Match> GetMatches(IEnumerable<Contour> contours)
        {
            List<Match> matches = new List<Match>();

            Parallel.ForEach(contours, contour =>
                {
                    Match match = GetMatch(contour);

                    if (match != null)
                    {
                        lock (matches)
                        {
                            matches.Add(match);
                        }
                    }
                });

            return matches;
        }
        /// <summary>
        /// Same as simple GetMatches implementation but allow to customize finded match handling and getting contour mechanism from any object
        /// </summary>
        public void GetMatches<T>(IEnumerable<T> dataSource, Func<T, Contour> getContourFucntion, Action<T, Match> matchHandler)
        {
            Parallel.ForEach(dataSource, data =>
            {
                Contour contour = getContourFucntion.Invoke(data);
                Match match = GetMatch(contour);

                if (match != null)
                {
                    matchHandler.Invoke(data, match);
                }
            });
        }
        private Match GetPotentialMatch(ContourInformation familiar, ComplexSequence bringed)
        {
            Complex ACF_NSP = familiar.Contour.ACF.AsComplexSequence().NormalizedScalarProduct(bringed.ACF.AsComplexSequence());

            if (ACF_NSP.AbsoluteValue() > m_minACF_NSP )
            {
                Complex maxNSP = familiar.Contour.MaximalNormalizedScalarProduct(bringed);

                if (maxNSP.AbsoluteValue() > m_minNsp )
                {
                    return new Match() { ACF_NSP = ACF_NSP, MaxNSP = maxNSP, FamiliarContour = familiar };
                }
            }

            return null;
        }        
        /// <summary>
        /// Brings all familiar contours to same lenght to speed up
        /// </summary>
        public void BringFamiliarContours(int targetLenght)
        {
            foreach (var cInfo in m_familiarContours)
            {
                cInfo.Contour = cInfo.Contour.BringLength(targetLenght);
            }
        }

    }
}
