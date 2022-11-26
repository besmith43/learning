using System;
using Qml.Net;

namespace nstest
{
    public class TestModel
    {
        private string _test = "";

        [NotifySignal]
        public string getTest
        {
            get
            {
                return _test;
            }
            set
            {
                if (_test == value)
                {
                    return;
                }

                _test = value;
                this.ActivateSignal("testChanged");
            }
        }

        public void ChangeTest()
        {
            getTest = "Testing Complete";
        }
    }
}