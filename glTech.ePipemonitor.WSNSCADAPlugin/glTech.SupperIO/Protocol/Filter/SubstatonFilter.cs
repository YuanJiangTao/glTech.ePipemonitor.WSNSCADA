using System;
using System.Collections.Generic;
using System.Text;

namespace glTech.ePipemonitor.WSNSCADAPlugin.glTech.SupperIO.Protocol.Filter
{
    class SubstatonFilter
    {
        public static SubstationRequestInfo Filter( List<byte> listRecvBuffer)
        {
            return new SubstationRequestInfo(listRecvBuffer.ToArray());
        }
    }
}
