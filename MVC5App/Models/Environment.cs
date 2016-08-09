using System;

namespace MVC5App.Models
{
    public class Environment : IEnvironment
    {
        public string Arctic { get; set; }
        public string Coastal { get; set; }
        public string Desert { get; set; }
        public string Forest { get; set; }
        public string Grassland { get; set; }
        public string Hill { get; set; }
        public string Mountain { get; set; }
        public string Swamp { get; set; }
        public string Underdark { get; set; }
        public string Underwater { get; set; }
        public string Urban { get; set; }


        private enum Env
        {
            Arctic,
            Coastal,
            Desert,
            Forest,
            Grassland,
            Hill,
            Mountain,
            Swamp,
            Underdark,
            Underwater,
            Urban
        }

        private bool ExistsInEnvironment(Env env)
        {
            switch (env)
            {
                case Env.Arctic:
                    return Arctic == "yes";
                case Env.Coastal:
                    return Coastal == "yes";
                case Env.Desert:
                    return Desert == "yes";
                case Env.Forest:
                    return Forest == "yes";
                case Env.Grassland:
                    return Grassland == "yes";
                case Env.Hill:
                    return Hill == "yes";
                case Env.Mountain:
                    return Mountain == "yes";
                case Env.Swamp:
                    return Swamp == "yes";
                case Env.Underdark:
                    return Underdark == "yes";
                case Env.Underwater:
                    return Underwater == "yes";
                case Env.Urban:
                    return Urban == "yes";
                default:
                    return true;
            }
        }
        public bool HasEnvironment(int environment)
        {
            return ExistsInEnvironment((Env)environment);
        }
    }
}