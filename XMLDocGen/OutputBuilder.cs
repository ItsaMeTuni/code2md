using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLDocGen
{
    public class OutputBuilder
    {
        public void ReconstructFromParts(ref string _base, List<ParsedPart> _parts, List<TypeData> _typeDatas)
        {
            /*
            for (int i = 0; i < _parts.Count; i++)
            {
                string partText = _parts[i].text;

                TagContext context;

                ReconstructConditionals(ref partText, _parts[i], context);
                ReconstructFromParts(ref partText, _parts[i].childParts, _typeDatas); //Recursion

                _base = _base.Insert(_parts[i].insertionIndex, partText);
                
            }
            */
        }

        public void ReconstructConditionals(ref string _base, ParsedPart _part, TagContext _context)
        {
            /*
            string tagVal = TemplateReplacer.GetTagValue(_context, _part.tag);

            if(tagVal == null || tagVal == "")
            {
                return;
            }
            else
            {
                _base = _base.Insert(_part.insertionIndex, _part.text);
            }
            */
        }
    }
}
