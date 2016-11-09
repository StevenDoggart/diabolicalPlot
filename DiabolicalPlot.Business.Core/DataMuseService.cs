using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace DiabolicalPlot.Business
{
    public class DataMuseService
    {
        public async Task<IEnumerable<string>> GetSynonymsAsync(string word)
        {
            using (var client = new HttpClient())
            {
                string result = await client.GetStringAsync($"https://api.datamuse.com/{EndPoints.Words}?{WordsParams.RelatedWord_Synonym}={word}");
                var serializer = new JsonSerializer();
                using (var stringReader = new StringReader(result))
                {
                    using (var jsonReader = new JsonTextReader(stringReader))
                    {
                        return serializer.Deserialize<Word[]>(jsonReader).Select(x => x.word);
                    }
                }
            }
        }


        private class EndPoints
        {
            public const string Words = "words";
            public const string Suggestions = "sug";
        }


        private class WordsParams
        {
            // Constraints
            public const string MeansLike = "ml";
            public const string SoundsLike = "sl";
            public const string SpelledLike = "sp";
            public const string RelatedWord_PopularNounsModifiedByAdjective = "rel_jja";
            public const string RelatedWord_PopularAdjectivesToModifyNoun = "rel_jjb";
            public const string RelatedWord_Synonym = "rel_syn";
            public const string RelatedWord_Antonym = "rel_ant";
            public const string RelatedWord_KindOf = "rel_spc";
            public const string RelatedWord_MoreGeneralThan = "rel_gen";
            public const string RelatedWord_Comprises = "rel_com";
            public const string RelatedWord_PartOf = "rel_par";
            public const string RelatedWord_FrequentFollowers = "rel_bga";
            public const string RelatedWord_FrequentPredecessors = "rel_bgb";
            public const string RelatedWord_Rhymes = "rel_rhy";
            public const string RelatedWord_ApproximateRhymes = "rel_nry";
            public const string RelatedWord_Homophones = "rel_hom";
            public const string RelatedWord_ConsonantMatch = "rel_cns";

            // Context hints
            public const string TopicWords = "topics";
            public const string LeftContext = "lc";
            public const string RightContext = "rc";

            public const string Vocabulary = "v";
            public const string MaximumResults = "max";
        }


        private class SuggestionsParams
        {
            public const string PrefixHint = "s";
            public const string MaximumResults = "max";
            public const string Vocabulary = "v";
        }


        private class Word
        {
            public string word { get; set; }
            public int score { get; set; }
        }
    }
    

    public interface IDataMuseService
    {
        Task<IEnumerable<string>> GetSynonymsAsync(string word);
    }
}
