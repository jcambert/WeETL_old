using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeETL.Schemas
{
    public class FileSystemEventSchema
    {

        //
        // Résumé :
        //     Gets the type of directory event that occurred.
        //
        // Retourne :
        //     One of the System.IO.WatcherChangeTypes values that represents the kind of change
        //     detected in the file system.
        public WatcherChangeTypes ChangeType { get; set; }
        //
        // Résumé :
        //     Gets the fully qualified path of the affected file or directory.
        //
        // Retourne :
        //     The path of the affected file or directory.
        public string FullPath { get; set; }
        //
        // Résumé :
        //     Gets the name of the affected file or directory.
        //
        // Retourne :
        //     The name of the affected file or directory.
        public string Name { get; set; }
    }
}
