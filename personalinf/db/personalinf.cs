using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personalinf.db
{
    class personalinfo
    {
        private string name = "";
        private string bloodtype = "";
        private string birthday = "";
        private string sex = "";
        private string home = "";
        private string hobby = "";
        private string selfintr = "";
        private string pic = "";

       
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Bloodtype
        {
            get { return bloodtype; }
            set { bloodtype = value; }
        }

        public string Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        public string Hobby
        {
            get { return hobby; }
            set { hobby = value; }
        }

        public string Home
        {
            get { return home; }
            set { home = value; }
        }

        public string Selfintr
        {
            get { return selfintr; }
            set { selfintr = value; }
        }

        public string Pic
        {
            get { return pic; }
            set { pic = value; }
        }

        public personalinfo()
        {

        }

        public personalinfo(string _name, string _bloodtype, string _birthday, string _sex, string _home, string _hobby, string _selfintr, string _pic)
        {
                 Name = _name;
                 Bloodtype = _birthday;
                 Birthday=_birthday;
                 Sex=_sex;
                 Home=_home;
                 Hobby=_hobby;
                 Selfintr=_selfintr;
                 Pic=_pic;
    }
    }
}
