 

using CZBK.BookShop.IBLL;
using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.BLL{
	
	public partial class ActionGroupService :BaseService<ActionGroup>,IActionGroupService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.ActionGroupDal;
        }
    }   
	
	public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.ActionInfoDal;
        }
    }   
	
	public partial class Articel_WordsService :BaseService<Articel_Words>,IArticel_WordsService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.Articel_WordsDal;
        }
    }   
	
	public partial class BookCommentService :BaseService<BookComment>,IBookCommentService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.BookCommentDal;
        }
    }   
	
	public partial class BooksService :BaseService<Books>,IBooksService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.BooksDal;
        }
    }   
	
	public partial class CartService :BaseService<Cart>,ICartService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.CartDal;
        }
    }   
	
	public partial class CategoriesService :BaseService<Categories>,ICategoriesService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.CategoriesDal;
        }
    }   
	
	public partial class CheckEmailService :BaseService<CheckEmail>,ICheckEmailService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.CheckEmailDal;
        }
    }   
	
	public partial class DepartmentService :BaseService<Department>,IDepartmentService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.DepartmentDal;
        }
    }   
	
	public partial class keyWordsRankService :BaseService<keyWordsRank>,IkeyWordsRankService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.keyWordsRankDal;
        }
    }   
	
	public partial class OrderBookService :BaseService<OrderBook>,IOrderBookService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.OrderBookDal;
        }
    }   
	
	public partial class OrdersService :BaseService<Orders>,IOrdersService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.OrdersDal;
        }
    }   
	
	public partial class PublishersService :BaseService<Publishers>,IPublishersService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.PublishersDal;
        }
    }   
	
	public partial class R_UserInfo_ActionInfoService :BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.R_UserInfo_ActionInfoDal;
        }
    }   
	
	public partial class RoleService :BaseService<Role>,IRoleService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.RoleDal;
        }
    }   
	
	public partial class SearchDetailsService :BaseService<SearchDetails>,ISearchDetailsService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.SearchDetailsDal;
        }
    }   
	
	public partial class SettingsService :BaseService<Settings>,ISettingsService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.SettingsDal;
        }
    }   
	
	public partial class SysFunService :BaseService<SysFun>,ISysFunService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.SysFunDal;
        }
    }   
	
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserInfoDal;
        }
    }   
	
	public partial class UsersService :BaseService<Users>,IUsersService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UsersDal;
        }
    }   
	
	public partial class UserStatesService :BaseService<UserStates>,IUserStatesService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserStatesDal;
        }
    }   
	
	public partial class VidoFileService :BaseService<VidoFile>,IVidoFileService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.VidoFileDal;
        }
    }   
	
}