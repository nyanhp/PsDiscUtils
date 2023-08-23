---
external help file: PsDiscUtils.dll-Help.xml
Module Name: PsDiscUtils
online version:
schema: 2.0.0
---

# New-IsoFile

## SYNOPSIS
Create ISO files

## SYNTAX

```
New-IsoFile [[-Path] <String[]>] [-DestinationPath] <String> [-VolumeIdentifier <String>] [-BootFile <String>]
 [-BootLoadSegment <Int32>] [-BootDeviceEmulation <BootDeviceEmulation>] [<CommonParameters>]
```

## DESCRIPTION
Create (bootable) ISO files using a list of paths. Folders are added as-is

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ChildItem ./customiso | New-IsoFile -DestinationPath C:\tmp\someiso.iso -BootFile C:\Windows\Boot\DVD\EFI\en-US\efisys.bin
```

Creates new bootable ISO file from the content of ./customiso

## PARAMETERS

### -BootDeviceEmulation
The boot device emulation type, if you know what you're doing. Default is NoEmulation

```yaml
Type: BootDeviceEmulation
Parameter Sets: (All)
Aliases:
Accepted values: NoEmulation, Diskette1200KiB, Diskette1440KiB, Diskette2880KiB, HardDisk

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BootFile
The boot file to apply

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BootLoadSegment
Which byte segment to load

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DestinationPath
ISO path

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Files and folders to add to the ISO

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -VolumeIdentifier
Optional Volume Identifier

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

## OUTPUTS

### System.IO.FileInfo

## NOTES

## RELATED LINKS
