
@{

# Script module or binary module file associated with this manifest.
RootModule = 'ProductivityTools.PSFlickr.dll'

# Version number of this module.
ModuleVersion = '0.0.7'

# ID used to uniquely identify this module
GUID = '80ab9930-8875-41d2-93fe-6fb78759b884'

# Author of this module
Author = 'Paweł Wujczyk'

# Description of the functionality provided by this module
Description = 'Mainly used to sync local directory with flickr account, but also allows to create and remove albums, push photos and others.'

# Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
CmdletsToExport = @('Authenticate-Flickr',
'Clear-Flickr',
'Remove-FlickrAlbum',
'Get-FlickrAlbums',
'Move-FlickrSinglePhotostoAlbum',
'New-FlickrAlbum',
'Add-FlickrPhoto',
'Sync-FlickrSet',
'Sync-FlickrAlbumFromDirectory',
'Set-FlickrAlbumPermissions',
'Set-FlickrPhotoPermissions')


# List of all files packaged with this module
FileList = @('.\MasterConfiguration.xml',
		'.\Description.dll',
		'.\Description.pdb',
		'.\Description.xml',
		'.\FlickrNet.dll',
		'.\ProductivityTools.MasterConfiguration.dll',
		'.\ProductivityTools.MasterConfiguration.pdb',
		'.\ProductivityTools.PSCmdlet.dll',
		'.\ProductivityTools.PSCmdlet.pdb',
		'.\ProductivityTools.PSFlickr.ApplicationClient.dll',
		'.\ProductivityTools.PSFlickr.ApplicationClient.pdb',
		'.\ProductivityTools.PSFlickr.dll',
		'.\ProductivityTools.PSFlickr.pdb',
		'.\ProductivityTools.PSFlickr.Configuration.dll',
		'.\ProductivityTools.PSFlickr.Configuration.pdb',
		'.\ProductivityTools.PSFlickr.FlickrProxy.dll',
		'.\ProductivityTools.PSFlickr.FlickrProxy.pdb',
		'.\PSFlickrCover.jpg')

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = @('Flickr','Photos')

        # A URL to the main website for this project.
        ProjectUri = 'http://productivitytools.tech/psflickr/'

    } # End of PSData hashtable

} # End of PrivateData hashtable

# HelpInfo URI of this module
HelpInfoURI = 'http://productivitytools.tech/psflickr/'
}

