using DiscUtils.Iso9660;
using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Management.Automation.Runspaces;
using System.Text;

namespace PsDiscUtils.Iso
{
    [Cmdlet(VerbsCommon.New, "PDUIsoFile", DefaultParameterSetName = "bootfile")]
    [OutputType(typeof(FileInfo))]
    public class NewIsoFileCommand : PSCmdlet
    {

        [Parameter(
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = true,
            ParameterSetName = "bootfile")]
        [Parameter(
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = true,
            ParameterSetName = "bootiso")]
        public string[] Path { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 1,
            ParameterSetName = "bootfile")]
        [Parameter(
            Mandatory = true,
            Position = 1,
            ParameterSetName = "bootiso")]
        public string DestinationPath { get; set; }

        [Parameter(ParameterSetName = "bootfile")]
        [Parameter(ParameterSetName = "bootiso")]
        public string VolumeIdentifier { get; set; }

        [Parameter(ParameterSetName = "bootfile")]
        [Parameter(ParameterSetName = "bootiso")]
        public SwitchParameter UseJoliet { get; set; }

        [Parameter(ParameterSetName = "bootfile")]
        public string BootFile { get; set; }

        [Parameter(ParameterSetName = "bootiso")]
        public string ReferenceBootableIso { get; set; }

        [Parameter(ParameterSetName = "bootfile")]
        [Parameter(ParameterSetName = "bootiso")]
        public int BootLoadSegment = 0;

        [Parameter(ParameterSetName = "bootfile")]
        [Parameter(ParameterSetName = "bootiso")]
        public BootDeviceEmulation BootDeviceEmulation = BootDeviceEmulation.NoEmulation;

        [Parameter(ParameterSetName = "bootfile")]
        [Parameter(ParameterSetName = "bootiso")]
        public SwitchParameter UpdateIsolinuxBootTable;

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
                if ((File.GetAttributes(item) & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    var folder = new DirectoryInfo(item);
                    foreach (var file in folder.GetFiles("*", SearchOption.AllDirectories))
                    {
                        _builder.AddFile(file.FullName.Replace(folder.Parent.FullName, string.Empty), file.FullName);
                    }
                    continue;
                }
                _builder.AddFile(new FileInfo(item).Name, item);
            }
        }

        protected override void EndProcessing()
        {
            switch (ParameterSetName)
            {
                case "bootfile":
                    {
                        if (!string.IsNullOrWhiteSpace(BootFile))
                        {
                            using (var fs = new FileStream(BootFile, FileMode.Open, FileAccess.Read))
                            {
                                _builder.SetBootImage(fs, BootDeviceEmulation, BootLoadSegment);
                                _builder.Build(DestinationPath);
                            }
                        };

                        WriteObject(new FileInfo(DestinationPath));
                        return;
                    }
                case "bootiso":
                    {
                        if (!string.IsNullOrWhiteSpace(ReferenceBootableIso))
                        {
                            using (var fs = new FileStream(ReferenceBootableIso, FileMode.Open, FileAccess.Read))
                            {
                                using (var reader = new CDReader(fs, UseJoliet))
                                {
                                    _builder.SetBootImage(reader.OpenBootImage(), BootDeviceEmulation, BootLoadSegment);
                                    _builder.UpdateIsolinuxBootTable = UpdateIsolinuxBootTable.IsPresent;
                                    _builder.Build(DestinationPath);
                                }
                            }
                        }

                        WriteObject(new FileInfo(DestinationPath));
                        return;
                    }
            }

            _builder.Build(DestinationPath);
            WriteObject(new FileInfo(DestinationPath));
        }
    }
}
