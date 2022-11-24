using Microsoft.AspNetCore.Mvc;

using testeLeadSoft.Data;

namespace testeLeadSoft.Controllers
{
	public class ArticleController : ControllerBase
	{
		private readonly DataContext context;
		public ArticleController(DataContext context) 
		{
			this.context = context;
		}
	}
}
