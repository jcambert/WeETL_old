using System.Collections.Generic;
using System.Diagnostics;

namespace WeETL.Observables.BySpeed
{
    [DebuggerDisplay("G28* Sub Prg {Command.L}:{Command.X}x{Command.Y}")]
    public class G28 : GCommand, IGProgramm
    {


        public List<GCommand> Commands { get; } = new List<GCommand>();
    }
    [DebuggerDisplay("G29*Encombrement :{Command.X}x{Command.Y}")]
    public class G29 : GCommand
    {

    }
    public class G40 : GCommand
    {

    }
    public class G41 : GCommand
    {

    }
    [DebuggerDisplay("G52*Appel Sous Programme {Command.L}")]
    public class G52 : GCommand
    {

    }
    public class G98 : GCommand
    {

    }
    [DebuggerDisplay("G99")]
    public class G99 : GCommand
    {

    }
    public class G0 : GCommand
    {

    }
    public class G1 : GCommand
    {

    }
    public class G2 : GCommand
    {

    }
    public class G3 : GCommand
    {

    }
}
