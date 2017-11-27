using Memindh;
using Model.Entities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ModuleA.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private MemHandler memHandler;
        private Timer timer;

        private Global global;
        private Unit unit;
        private string message;

        public Global Global
        {
            get
            {
                return global;
            }
            set
            {
                SetProperty(ref global, value);
            }
        }
        public Unit Unit
        {
            get
            {
                return unit;
            }
            set
            {
                SetProperty(ref unit, value);
            }
        }
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                SetProperty(ref message, value);
            }
        }

        public ViewAViewModel()
        {
            memHandler = new MemHandler("game");

            Global = new Global(memHandler);
            global.Update();
            Unit = global.SelectedUnitList[0] as Unit;

            timer = new Timer(200);
            timer.Elapsed += HandleTimer;
            timer.AutoReset = true;
            timer.Start();
        }

        private void HandleTimer(object sender, ElapsedEventArgs e)
        {
            try
            {
                global.Update();
                if (Global.SelectedUnitCount > 0)
                {
                    unit = global.SelectedUnitList[0] as Unit;
                    unit.Update();
                }
            }
            catch (Exception exception)
            {
                Message = exception.Message;
            }
        }

    }
}
