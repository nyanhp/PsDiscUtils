using DiscUtils.Iso9660;
using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace PsDiscUtils.Iso
{
    [Cmdlet(VerbsCommon.New, "IsoFile")]
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

        protected override void ProcessRecord()
        {
            CDBuilder builder = new CDBuilder
            {
                UseJoliet = UseJoliet.IsPresent,
                VolumeIdentifier = string.IsNullOrWhiteSpace(VolumeIdentifier) ? "NewIso" : VolumeIdentifier
            };
            foreach (var item in Path)
            {
                bool isDir = (File.GetAttributes(item) & FileAttributes.Directory) == FileAttributes.Directory;
                if (isDir)
                {
                    builder.AddDirectory(item);
                    continue;
                }
                builder.AddFile(new FileInfo(item).Name, item);
            }

            if (!string.IsNullOrWhiteSpace(BootFile))
            {
                builder.SetBootImage(new FileStream(BootFile, FileMode.Open, FileAccess.Read), BootDeviceEmulation, BootLoadSegment);
            }

            builder.Build(DestinationPath);
            WriteObject(new FileInfo(DestinationPath));
        }
    }
}
