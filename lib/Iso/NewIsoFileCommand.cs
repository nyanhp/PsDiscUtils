using DiscUtils.Iso9660;
using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace PsDiscUtils.Iso
{
    [Cmdlet(VerbsCommon.New, "PDUIsoFile")]
    [OutputType(typeof(FileInfo))]
    public class NewIsoFileCommand : PSCmdlet
    {

        [Parameter(
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = true)]
        public string[] Path { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 1)]
        public string DestinationPath { get; set; }

        [Parameter()]
        public string VolumeIdentifier { get; set; }

        [Parameter()]
        SwitchParameter UseJoliet { get; set; }

        [Parameter()]
        public string BootFile { get; set; }

        [Parameter()]
        public int BootLoadSegment = 0;

        [Parameter()]
        public BootDeviceEmulation BootDeviceEmulation = BootDeviceEmulation.NoEmulation;

        private CDBuilder _builder;

        protected override void BeginProcessing()
        {
            _builder = new CDBuilder
            {
                UseJoliet = UseJoliet.IsPresent,
                VolumeIdentifier = string.IsNullOrWhiteSpace(VolumeIdentifier) ? "NewIso" : VolumeIdentifier
            };
        }

        protected override void ProcessRecord()
        {
            foreach (var item in Path)
            {
                bool isDir = (File.GetAttributes(item) & FileAttributes.Directory) == FileAttributes.Directory;
                if (isDir)
                {
                    _builder.AddDirectory(item);
                    continue;
                }
                _builder.AddFile(new FileInfo(item).Name, item);
            }

            if (!string.IsNullOrWhiteSpace(BootFile))
            {
                _builder.SetBootImage(new FileStream(BootFile, FileMode.Open, FileAccess.Read), BootDeviceEmulation, BootLoadSegment);
            }
        }

        protected override void EndProcessing()
        {
            _builder.Build(DestinationPath);
            WriteObject(new FileInfo(DestinationPath));
        }
    }
}
