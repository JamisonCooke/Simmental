using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Command
{
    public class UpdateNpc : CommandBase
    {
        private readonly ICharacter _oldNpc;
        private readonly ICharacter _newNpc;
        private readonly Action _postUpdateAction;

        public UpdateNpc(ICharacter oldNpc, ICharacter newNpc, Action postUpdateAction)
        {
            _oldNpc = oldNpc;
            _newNpc = newNpc;
            _postUpdateAction = postUpdateAction;
        }

        public override void Execute(IGame game)
        {
            if (_oldNpc != null && game.NPC.Contains(_oldNpc))
                game.NPC.Remove(_oldNpc);
            if (_newNpc != null)
                game.NPC.Add(_newNpc);
            _postUpdateAction();
        }
    }
}
