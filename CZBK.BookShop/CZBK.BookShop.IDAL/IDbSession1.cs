 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.IDAL
{
	public partial interface IDBSession
    {

	
		IActionGroupDal ActionGroupDal{get;set;}
	
		IActionInfoDal ActionInfoDal{get;set;}
	
		IArticel_WordsDal Articel_WordsDal{get;set;}
	
		IBookCommentDal BookCommentDal{get;set;}
	
		IBooksDal BooksDal{get;set;}
	
		ICartDal CartDal{get;set;}
	
		ICategoriesDal CategoriesDal{get;set;}
	
		ICheckEmailDal CheckEmailDal{get;set;}
	
		IDepartmentDal DepartmentDal{get;set;}
	
		IkeyWordsRankDal keyWordsRankDal{get;set;}
	
		IOrderBookDal OrderBookDal{get;set;}
	
		IOrdersDal OrdersDal{get;set;}
	
		IPublishersDal PublishersDal{get;set;}
	
		IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal{get;set;}
	
		IRoleDal RoleDal{get;set;}
	
		ISearchDetailsDal SearchDetailsDal{get;set;}
	
		ISettingsDal SettingsDal{get;set;}
	
		ISysFunDal SysFunDal{get;set;}
	
		IUserInfoDal UserInfoDal{get;set;}
	
		IUsersDal UsersDal{get;set;}
	
		IUserStatesDal UserStatesDal{get;set;}
	
		IVidoFileDal VidoFileDal{get;set;}
	}	
}