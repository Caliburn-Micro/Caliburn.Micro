using System;

namespace CM.HelloUniversal.ViewModels
{
    public class CharacterViewModel
    {
        public CharacterViewModel(string name, string image)
        {
            Name = name;
            Image = image;
        }

        public string Name
        {
            get;
            private set;
        }

        public string Image
        {
            get;
            private set;
        }
    }
}
