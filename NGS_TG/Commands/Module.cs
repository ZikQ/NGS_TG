using System.Reflection;
using NGS_TG.Commands.Attributes;

namespace NGS_TG.Commands
{
    public abstract class Module
    {
        public int Id { get; private set; }
        public abstract string Name { get; set; }
        public Bot Bot { get; private set; }
        public static List<Module> Modules { get; private set; } = new List<Module>();
        public List<Command> Commands { get; private set; } = new();
        public abstract void OnLoad();
        internal static void Register<T>(Bot bot) where T : Module, new()
        {
            var obj = new T();

            obj.Id = Modules.Count + 1;

            MethodInfo[] methods = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0)
                .ToArray();

            foreach (var method in methods)
            {
                Command attribute = Command.ToCommand((CommandAttribute)method.GetCustomAttributes(typeof(CommandAttribute), false)[0], method);

                attribute.Checkers = method.GetCustomAttributes(typeof(CheckBaseAttribute), false)
                    .Select(attr => (CheckBaseAttribute)attr)
                    .Select(checkAttribute => checkAttribute.GetType().GetMethod("ExecuteCheckAsync"))
                    .ToList();

                obj.Commands.Add(attribute);
            }

            Modules.Add(obj);

            obj.Bot = bot;
            obj.OnLoad();
        }
        public static Command FindCommandByName(string name)
        {
            var foundCommand = Modules
                .SelectMany(module => module.Commands)
                .FirstOrDefault(cmd => cmd.Name == name);

            return foundCommand;
        }
    }
}
