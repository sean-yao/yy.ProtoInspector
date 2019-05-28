using Fiddler;
using Standard;

namespace yy.ProtoInspector
{
    public class ProtoRequestViewer : ProtoViewer, IRequestInspector2
    {
        public HTTPRequestHeaders headers
        {
            get
            {
                return null;
            }
            set
            {
                this.OriginalHeaders = value;
            }
        }

        protected override dynamic CreateJsonViewer()
        {
            return new JSONRequestViewer();
        }
    }
}
