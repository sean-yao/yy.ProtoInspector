using Fiddler;
using Standard;

namespace yy.ProtoInspector
{
    public class ProtoResponseViewer : ProtoViewer, IResponseInspector2
    {
        public HTTPResponseHeaders headers
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
            return new JSONResponseViewer();
        }
    }
}
