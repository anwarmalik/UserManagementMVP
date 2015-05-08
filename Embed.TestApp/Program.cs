using Autofac;
using Embed.TestApp.Model;
using Embed.TestApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Embed.TestApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();

            builder.Register(context => new EmbedContext("EmbedDb"))
                .As<IEmbedContext>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<UsersForm>().SingleInstance();

            var container = builder.Build();
            var form = container.Resolve<UsersForm>();

            Application.Run(form);
        }
    }
}
