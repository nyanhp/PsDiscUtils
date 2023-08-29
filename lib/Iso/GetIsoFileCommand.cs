using DiscUtils.Iso9660;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Text;

namespace PsDiscUtils.Iso
{
    [Cmdlet(VerbsCommon.Get, "PDUIsoFile")]
    [OutputType(typeof(FileInfo))]
    public class GetIsoFileCommand : PSCmdlet
    {
        [Parameter(
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = true)]
        public string Path { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(new CDReader(new FileStream(Path, FileMode.Open, FileAccess.Read), true));
        }
    }
}
