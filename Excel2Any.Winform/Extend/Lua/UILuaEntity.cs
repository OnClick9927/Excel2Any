
namespace Excel2Any.Winform
{
    [Entity(typeof(LuaEntity))]
    public class UILuaEntity : UIEntity
    {
        public override string name { get; } = "Lua";

        public override BaseConvertPage page { get; } = new LuaConvertPage();

        public override BaseSetting setting { get; set; } = new LuaSetting();

        public override int symbol { get; } = 261897;

        public override int pageIndex { get; } = 1006;
    }
}
