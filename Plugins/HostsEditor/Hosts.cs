using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HostsEditor
{
    class Hosts
    {
        class BaseElement {
            public virtual string Extract() {
                throw new NotImplementedException();
            } //write to file
        }
        class CommentElement: BaseElement{
            public CommentElement( string _comment ) { comment = _comment; }
            public string comment;
            public override string Extract() => "#" + comment + "\r\n";
        }
        class IpElement : BaseElement {
            public IpElement( string _a) { address = IPAddress.Parse(_a); }
            public IPAddress address;
            public override string Extract() => address.ToString() + "\t"; //add tab after ip address
        }
        class DomainElement : BaseElement {
            public DomainElement( string _d) { domain = _d; }
            public string domain;
            public override string Extract()=> domain + "\t";
        }
        class UnckonwElement : BaseElement {
            public UnckonwElement(string _s) { unparsed = _s; }
            public string unparsed;
            public override string Extract() => unparsed + "\t";
        }
        class EmptyLine : BaseElement {
            public override string Extract() => "\r\n";
        }

        List<BaseElement> _hostsElements = new List<BaseElement>();


        public Hosts()
        {
            ParseFile();
        }

        const string COMMENT = "COMMENT";
        const string ELEMENT = "ELEMENT";
        private void ParseFile()
        {
            Tokenizer tokenizer = Tokenizer.Empty
                .WithToken(COMMENT, @"(#.*)")
                .WithToken(ELEMENT, @"(\S+)");

            using (StreamReader sr = new StreamReader(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.System),
                    "drivers/etc/hosts")
                    )
                )
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Token[] tokens = tokenizer.Tokenize(line);

                    if (tokens.Length == 0)
                    {
                        _hostsElements.Add(new EmptyLine());
                        continue;
                    }

                    if (tokens.Length >= 2 && tokens[0].Type == ELEMENT && tokens[1].Type == ELEMENT)
                    {
                        //add ip and host
                        _hostsElements.Add(new IpElement(tokens[0].Value));
                        _hostsElements.Add(new DomainElement(tokens[1].Value));
                    }
                    else
                    {
                        //all add to uncknown
                        foreach (Token _t in tokens)
                        {
                            if (_t.Type == COMMENT)
                                _hostsElements.Add(new CommentElement(_t.Value));
                            else
                                _hostsElements.Add(new UnckonwElement(_t.Value));
                        }
                    }
                }
            }
        }
        private void WriteFile()
        {
            using (StreamWriter sr = new StreamWriter(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.System),
                    "drivers/etc/hosts")
                    )
                )
            {
                foreach (BaseElement _e in _hostsElements)
                    sr.Write(_e.Extract());
            }
        }

        IPAddress FindDomain( string domain )
        {
            return null;
        }

        //if exists - return false.
        bool AddRecord( IPAddress _address, string domain, bool replace = false )
        {
            if (FindDomain(domain) != null)
                return false;
            return true;
        }
    }
}
