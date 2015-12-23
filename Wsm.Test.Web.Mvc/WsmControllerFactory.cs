using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Mvc;

namespace Wsm.Test.Web.Mvc
{
    internal class WsmControllerFactory : DefaultControllerFactory
    {
        private readonly CompositionContainer _container;

        public WsmControllerFactory(CompositionContainer container)
        {
            this._container = container;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            Lazy<object, object> export = _container.GetExports(controllerType, null, null).FirstOrDefault();

            //here if the controller object is not found (resulted as null) we can go ahead and get the default controller.
            //This means that in order to get the Controller we have to Export it first which will be shown later in this post
            return null == export ? base.GetControllerInstance(requestContext, controllerType) : (IController)export.Value;
        }
        public override void ReleaseController(IController controller)
        {
            //this is were the controller gets disposed
            ((IDisposable)controller).Dispose();
        }
    }
}