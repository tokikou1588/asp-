 

using CZBK.BookShop.DAL;
using CZBK.BookShop.IDAL;
using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CZBK.BookShop.DALFactory
{
	public partial class DBSession : IDBSession
    {
	
		private IActionGroupDal _ActionGroupDal;
        public IActionGroupDal ActionGroupDal
        {
            get
            {
                if(_ActionGroupDal == null)
                {
                    _ActionGroupDal = AbstractFactory.CreateActionGroupDal();
                }
                return _ActionGroupDal;
            }
            set { _ActionGroupDal = value; }
        }
	
		private IActionInfoDal _ActionInfoDal;
        public IActionInfoDal ActionInfoDal
        {
            get
            {
                if(_ActionInfoDal == null)
                {
                    _ActionInfoDal = AbstractFactory.CreateActionInfoDal();
                }
                return _ActionInfoDal;
            }
            set { _ActionInfoDal = value; }
        }
	
		private IArticel_WordsDal _Articel_WordsDal;
        public IArticel_WordsDal Articel_WordsDal
        {
            get
            {
                if(_Articel_WordsDal == null)
                {
                    _Articel_WordsDal = AbstractFactory.CreateArticel_WordsDal();
                }
                return _Articel_WordsDal;
            }
            set { _Articel_WordsDal = value; }
        }
	
		private IBookCommentDal _BookCommentDal;
        public IBookCommentDal BookCommentDal
        {
            get
            {
                if(_BookCommentDal == null)
                {
                    _BookCommentDal = AbstractFactory.CreateBookCommentDal();
                }
                return _BookCommentDal;
            }
            set { _BookCommentDal = value; }
        }
	
		private IBooksDal _BooksDal;
        public IBooksDal BooksDal
        {
            get
            {
                if(_BooksDal == null)
                {
                    _BooksDal = AbstractFactory.CreateBooksDal();
                }
                return _BooksDal;
            }
            set { _BooksDal = value; }
        }
	
		private ICartDal _CartDal;
        public ICartDal CartDal
        {
            get
            {
                if(_CartDal == null)
                {
                    _CartDal = AbstractFactory.CreateCartDal();
                }
                return _CartDal;
            }
            set { _CartDal = value; }
        }
	
		private ICategoriesDal _CategoriesDal;
        public ICategoriesDal CategoriesDal
        {
            get
            {
                if(_CategoriesDal == null)
                {
                    _CategoriesDal = AbstractFactory.CreateCategoriesDal();
                }
                return _CategoriesDal;
            }
            set { _CategoriesDal = value; }
        }
	
		private ICheckEmailDal _CheckEmailDal;
        public ICheckEmailDal CheckEmailDal
        {
            get
            {
                if(_CheckEmailDal == null)
                {
                    _CheckEmailDal = AbstractFactory.CreateCheckEmailDal();
                }
                return _CheckEmailDal;
            }
            set { _CheckEmailDal = value; }
        }
	
		private IDepartmentDal _DepartmentDal;
        public IDepartmentDal DepartmentDal
        {
            get
            {
                if(_DepartmentDal == null)
                {
                    _DepartmentDal = AbstractFactory.CreateDepartmentDal();
                }
                return _DepartmentDal;
            }
            set { _DepartmentDal = value; }
        }
	
		private IkeyWordsRankDal _keyWordsRankDal;
        public IkeyWordsRankDal keyWordsRankDal
        {
            get
            {
                if(_keyWordsRankDal == null)
                {
                    _keyWordsRankDal = AbstractFactory.CreatekeyWordsRankDal();
                }
                return _keyWordsRankDal;
            }
            set { _keyWordsRankDal = value; }
        }
	
		private IOrderBookDal _OrderBookDal;
        public IOrderBookDal OrderBookDal
        {
            get
            {
                if(_OrderBookDal == null)
                {
                    _OrderBookDal = AbstractFactory.CreateOrderBookDal();
                }
                return _OrderBookDal;
            }
            set { _OrderBookDal = value; }
        }
	
		private IOrdersDal _OrdersDal;
        public IOrdersDal OrdersDal
        {
            get
            {
                if(_OrdersDal == null)
                {
                    _OrdersDal = AbstractFactory.CreateOrdersDal();
                }
                return _OrdersDal;
            }
            set { _OrdersDal = value; }
        }
	
		private IPublishersDal _PublishersDal;
        public IPublishersDal PublishersDal
        {
            get
            {
                if(_PublishersDal == null)
                {
                    _PublishersDal = AbstractFactory.CreatePublishersDal();
                }
                return _PublishersDal;
            }
            set { _PublishersDal = value; }
        }
	
		private IR_UserInfo_ActionInfoDal _R_UserInfo_ActionInfoDal;
        public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
        {
            get
            {
                if(_R_UserInfo_ActionInfoDal == null)
                {
                    _R_UserInfo_ActionInfoDal = AbstractFactory.CreateR_UserInfo_ActionInfoDal();
                }
                return _R_UserInfo_ActionInfoDal;
            }
            set { _R_UserInfo_ActionInfoDal = value; }
        }
	
		private IRoleDal _RoleDal;
        public IRoleDal RoleDal
        {
            get
            {
                if(_RoleDal == null)
                {
                    _RoleDal = AbstractFactory.CreateRoleDal();
                }
                return _RoleDal;
            }
            set { _RoleDal = value; }
        }
	
		private ISearchDetailsDal _SearchDetailsDal;
        public ISearchDetailsDal SearchDetailsDal
        {
            get
            {
                if(_SearchDetailsDal == null)
                {
                    _SearchDetailsDal = AbstractFactory.CreateSearchDetailsDal();
                }
                return _SearchDetailsDal;
            }
            set { _SearchDetailsDal = value; }
        }
	
		private ISettingsDal _SettingsDal;
        public ISettingsDal SettingsDal
        {
            get
            {
                if(_SettingsDal == null)
                {
                    _SettingsDal = AbstractFactory.CreateSettingsDal();
                }
                return _SettingsDal;
            }
            set { _SettingsDal = value; }
        }
	
		private ISysFunDal _SysFunDal;
        public ISysFunDal SysFunDal
        {
            get
            {
                if(_SysFunDal == null)
                {
                    _SysFunDal = AbstractFactory.CreateSysFunDal();
                }
                return _SysFunDal;
            }
            set { _SysFunDal = value; }
        }
	
		private IUserInfoDal _UserInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if(_UserInfoDal == null)
                {
                    _UserInfoDal = AbstractFactory.CreateUserInfoDal();
                }
                return _UserInfoDal;
            }
            set { _UserInfoDal = value; }
        }
	
		private IUsersDal _UsersDal;
        public IUsersDal UsersDal
        {
            get
            {
                if(_UsersDal == null)
                {
                    _UsersDal = AbstractFactory.CreateUsersDal();
                }
                return _UsersDal;
            }
            set { _UsersDal = value; }
        }
	
		private IUserStatesDal _UserStatesDal;
        public IUserStatesDal UserStatesDal
        {
            get
            {
                if(_UserStatesDal == null)
                {
                    _UserStatesDal = AbstractFactory.CreateUserStatesDal();
                }
                return _UserStatesDal;
            }
            set { _UserStatesDal = value; }
        }
	
		private IVidoFileDal _VidoFileDal;
        public IVidoFileDal VidoFileDal
        {
            get
            {
                if(_VidoFileDal == null)
                {
                    _VidoFileDal = AbstractFactory.CreateVidoFileDal();
                }
                return _VidoFileDal;
            }
            set { _VidoFileDal = value; }
        }
	}	
}