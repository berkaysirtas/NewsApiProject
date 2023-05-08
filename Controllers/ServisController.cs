using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsApiProject.Models;
using NewsApiProject.ViewModel;

namespace NewsApiProject.Controllers
{
    public class ServisController : ApiController
    {
        newsDbEntities db = new newsDbEntities();
        InfoModel info = new InfoModel();

        #region Category

        [HttpGet]
        [Route("api/kategoriliste")]
        public List<CategoryModel> KategoriListe()
        {
            List<CategoryModel> liste = GetCategory().Select(x => new CategoryModel()
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                CatNewsNumber = x.News.Count()

            }).ToList();

            return liste;

            System.Data.Entity.DbSet<Category> GetCategory()
            {
                return db.Category;
            }
        }

        [HttpGet]
        [Route("api/kategoribyid/{katId}")]
        public CategoryModel KategoriById(int katId)
        {
            CategoryModel kayit = db.Category.Where(s => s.CategoryId == katId).Select(x => new CategoryModel()
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                CatNewsNumber = x.News.Count()

            }).SingleOrDefault();

            return kayit;
        }


        [HttpPost]
        [Route("api/kategoriekle")]
        public InfoModel KategoriEkle(CategoryModel model)
        {
            if (db.Category.Count(s => s.CategoryName == model.CategoryName) > 0)
            {
                info.process = false;
                info.message = "Girilen Kategori AdıKayıtlıdır!";
                return info;
            }

            Category yeni = new Category();
            yeni.CategoryName = model.CategoryName;

            db.Category.Add(yeni);
            db.SaveChanges();

            info.process = true;
            info.message = "Kategori Eklendi";

            return info;
        }

        [HttpPut]
        [Route("api/kategoriduzenle")]
        public InfoModel KategoriDuzenle(CategoryModel model)
        {
            Category kayit = db.Category.Where(s => s.CategoryId == model.CategoryId).FirstOrDefault();
            if (kayit == null)
            {
                info.process = false;
                info.message = "KayıtBulunamadı!";
                return info;
            }

            kayit.CategoryName = model.CategoryName;
            db.SaveChanges();

            info.process = true;
            info.message = "Kategori Düzenlendi";

            return info;
        }

        [HttpDelete]
        [Route("api/kategorisil/{CategoryId}")]
        public InfoModel KategoriSil(int CategoryId)
        {
            Category kayit = db.Category.Where(s => s.CategoryId == CategoryId).FirstOrDefault();
            if (kayit == null)
            {
                info.process = false;
                info.message = "KayıtBulunamadı!";
                return info;
            }
            if (db.News.Count(s => s.CategoryId == CategoryId) > 0)
            {
                info.process = false;
                info.message = "Üzerinde Haber Kayıtlı Kategori Silinemez!";
                return info;
            }

            db.Category.Remove(kayit);
            db.SaveChanges();

            info.process = true;
            info.message = "Kategori Silindi";

            return info;
        }


        #endregion



        #region News

        [HttpGet]
        [Route("api/haberliste")]
        public List<NewsModel> HaberListe()
        {
            List<NewsModel> liste = db.News.Select(x => new NewsModel()
            {
                NewsId = x.NewsId,
                Headline = x.Headline,
                Content = x.Content,
                PhotoUrl = x.PhotoUrl,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.CategoryName,
                ReadNumber = x.ReadNumber,
                Date = x.Date,
                MemberId = x.MemberId,
                NickName = x.Members.UsersName

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/haberlistesoneklenenler/{s}")]
        public List<NewsModel> HaberListeSonEklenenler(int s)
        {
            List<NewsModel> liste = db.News.OrderByDescending(o => o.NewsId).Take(s).Select(x => new NewsModel()
            {

                NewsId = x.NewsId,
                Headline = x.Headline,
                Content = x.Content,
                PhotoUrl = x.PhotoUrl,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.CategoryName,
                ReadNumber = x.ReadNumber,
                Date = x.Date,
                MemberId = x.MemberId,
                NickName = x.Members.UsersName

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/haberlistebykatid/{CategoryId}")]
        public List<NewsModel> HaberListeByKatId(int CategoryId)
        {
            List<NewsModel> liste = db.News.Where(s => s.CategoryId == CategoryId).Select(x => new NewsModel()
            {
                NewsId = x.NewsId,
                Headline = x.Headline,
                Content = x.Content,
                PhotoUrl = x.PhotoUrl,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.CategoryName,
                ReadNumber = x.ReadNumber,
                Date = x.Date,
                MemberId = x.MemberId,
                NickName = x.Members.UsersName

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/haberlistebyuyeid/{MemberId}")]
        public List<NewsModel> MakaleListeByUyeId(int MemberId)
        {
            List<NewsModel> liste = db.News.Where(s => s.MemberId == MemberId).Select(x => new NewsModel()
            {
                NewsId = x.NewsId,
                Headline = x.Headline,
                Content = x.Content,
                PhotoUrl = x.PhotoUrl,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.CategoryName,
                ReadNumber = x.ReadNumber,
                Date = x.Date,
                MemberId = x.MemberId,
                NickName = x.Members.UsersName


            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/haberbyid/{haberId}")]
        public NewsModel HaberById(int NewsId)
        {
            NewsModel kayit = db.News.Where(s => s.NewsId == NewsId).Select(x => new NewsModel()
            {
                NewsId = x.NewsId,
                Headline = x.Headline,
                Content = x.Content,
                PhotoUrl = x.PhotoUrl,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.CategoryName,
                ReadNumber = x.ReadNumber,
                Date = x.Date,
                MemberId = x.MemberId,
                NickName = x.Members.UsersName

            }).SingleOrDefault();

            return kayit;

        }

        [HttpPost]
        [Route("api/haberekle")]
        public InfoModel HaberEkle(NewsModel model)
        {
            if (db.News.Count(s => s.Headline == model.Headline) > 0)
            {
                info.process = false;
                info.message = "Girilen Haber Başlığı Kayıtlıdır!";
                return info;
            }
            News yeni = new News();
            yeni.Headline = model.Headline;
            yeni.Content = model.Content;
            yeni.Date = model.Date;
            yeni.ReadNumber = model.ReadNumber;
            yeni.CategoryId = model.CategoryId;
            yeni.MemberId = model.MemberId;
            yeni.PhotoUrl = model.PhotoUrl;

            db.News.Add(yeni);
            db.SaveChanges();

            info.process = true;
            info.message = "Haber Eklendi";

            return info;
        }

        [HttpPut]
        [Route("api/haberduzenle")]
        public InfoModel HaberDuzenle(NewsModel model)
        {
            News kayit = db.News.Where(s => s.NewsId == model.NewsId).SingleOrDefault();
            if (kayit == null)
            {
                info.process = false;
                info.message = "Kayıt Bulunamadı!";
                return info;
            }

            kayit.Headline = model.Headline;
            kayit.Content = model.Content;
            kayit.Date = model.Date;
            kayit.ReadNumber = model.ReadNumber;
            kayit.CategoryId = model.CategoryId;
            kayit.MemberId = model.MemberId;
            kayit.PhotoUrl = model.PhotoUrl;

            db.News.Add(kayit);
            db.SaveChanges();

            info.process = true;
            info.message = "Haber Eklendi";

            return info;
        }

        [HttpDelete]
        [Route("api/habersil/{haberId}")]
        public InfoModel HaberSil(int haberId)
        {
            News kayit = db.News.Where(s => s.NewsId == haberId).SingleOrDefault();
            if (kayit == null)
            {
                info.process = false;
                info.message = "Kayıt Bulunamadı!";
                return info;
            }
            if (db.Comment.Count(s => s.NewsId == haberId) > 0)
            {
                info.process = false;
                info.message = "Üzerinde Yorum Kayıtlı Haber Silinemez!";
                return info;
            }

            db.News.Remove(kayit);
            db.SaveChanges();

            info.process = true;
            info.message = "Haber Silindi";

            return info;
        }

        #endregion


        #region Members

        [HttpGet]
        [Route("api/uyeliste")]
        public List<MembersModel> UyeListe()
        {
            List<MembersModel> liste = db.Members.Select(x => new MembersModel()
            {
                MemberId = x.MemberId,
                NameSurname = x.NameSurname,
                Email = x.Email,
                UsersName = x.UsersName,
                Pasword = x.Pasword,
                MemberAdmin = x.MemberAdmin

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public MembersModel UyeById(int uyeId)
        {
            MembersModel kayit = db.Members.Where(s => s.MemberId == uyeId).Select(x => new MembersModel()
            {
                MemberId = x.MemberId,
                NameSurname = x.NameSurname,
                Email = x.Email,
                UsersName = x.UsersName,
                Pasword = x.Pasword,
                MemberAdmin = x.MemberAdmin

            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/uyeekle")]
        public InfoModel UyeEkle(MembersModel model)
        {
            if (db.Members.Count(s => s.UsersName == model.UsersName || s.Email == model.Email) > 0)
            {
                info.process = false;
                info.message = "Girilen KullanıcıAdı veya E-Posta Adresi Kayıtlıdır!";
                return info;
            }
            Members yeni = new Members();
            yeni.NameSurname = model.NameSurname;
            yeni.Email = model.Email;
            yeni.UsersName = model.UsersName;
            yeni.Pasword = model.Pasword;
            yeni.MemberAdmin = model.MemberAdmin;

            db.Members.Add(yeni);
            db.SaveChanges();

            info.process = true;
            info.message = "Üye Eklendi";

            return info;
        }

        [HttpPut]
        [Route("api/uyeduzenle")]
        public InfoModel UyeDuzenle(MembersModel model)
        {
            Members kayit = db.Members.Where(s => s.MemberId == model.MemberId).SingleOrDefault();
            if (kayit == null)
            {
                info.process = false;
                info.message = "KayıtBulunamadı";
                return info;
            }

            kayit.NameSurname = model.NameSurname;
            kayit.Email = model.Email;
            kayit.UsersName = model.UsersName;
            kayit.Pasword = model.Pasword;
            kayit.MemberAdmin = model.MemberAdmin;
           

            db.SaveChanges();
            info.process = true;
            info.message = "Üye Düzenlendi";

            return info;
        }

        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public InfoModel UyeSil(int uyeId)
        {
            Members kayit = db.Members.Where(s => s.MemberId == uyeId).SingleOrDefault();
            if (kayit == null)
            {
                info.process = false;
                info.message = "KayıtBulunamadı";
                return info;
            }
            if (db.News.Count(s => s.MemberId == uyeId) > 0)
            {
                info.process = false;
                info.message = "Üzerinde Haber Kaydı Olan Üye Silinemez!";
                return info;
            }
            if (db.Comment.Count(s => s.MemberId == uyeId) > 0)
            {
                info.process = false;
                info.message = "Üzerinde Yorum Kaydı Olan Üye Silinemez!";
                return info;
            }
            db.Members.Remove(kayit);
            db.SaveChanges();

            info.process = true;
            info.message = "Üye Silindi";

            return info;
        }

        #endregion

        #region Comment

        [HttpGet]
        [Route("api/yorumliste")]
        public List<CommentModel> YorumListe()
        {
            List<CommentModel> liste = db.Comment.Select(x => new CommentModel()
            {
                CommentId = x.CommentId,
                CommentContent = x.CommentContent,
                NewsId = x.NewsId,
                MemberId = x.MemberId,
                Date = x.Date,
                UsersName = x.Members.UsersName,
                NewsHeadline = x.News.Headline,

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/yorumlistebyuyeid/{uyeId}")]
        public List<CommentModel> YorumListeByUyeId(int uyeId)
        {
            List<CommentModel> liste = db.Comment.Where(s => s.MemberId == uyeId).Select(x => new CommentModel()
            {
                CommentId = x.CommentId,
                CommentContent = x.CommentContent,
                NewsId = x.NewsId,
                MemberId = x.MemberId,
                Date = x.Date,
                UsersName = x.Members.UsersName,
                NewsHeadline = x.News.Headline,

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/yorumlistebyhaberid/{haberId}")]
        public List<CommentModel> YorumListeByhaberId(int haberId)
        {
            List<CommentModel> liste = db.Comment.Where(s => s.NewsId == haberId).Select(x => new CommentModel()
            {
                CommentId = x.CommentId,
                CommentContent = x.CommentContent,
                NewsId = x.NewsId,
                MemberId = x.MemberId,
                Date = x.Date,
                UsersName = x.Members.UsersName,
                NewsHeadline = x.News.Headline,

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/yorumlistesoneklenenler/{s}")]
        public List<CommentModel> YorumListeSonEklenenler(int s)
        {
            List<CommentModel> liste = db.Comment.OrderByDescending(o => o.NewsId).Take(s).Select(x => new CommentModel()
            {
                CommentId = x.CommentId,
                CommentContent = x.CommentContent,
                NewsId = x.NewsId,
                MemberId = x.MemberId,
                Date = x.Date,
                UsersName = x.Members.UsersName,
                NewsHeadline = x.News.Headline,

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/yorumbyid/{yorumId}")]
        public CommentModel YorumById(int yorumId)
        {
            CommentModel kayit = db.Comment.Where(s => s.CommentId == yorumId).Select(x => new CommentModel()
            {
                CommentId = x.CommentId,
                CommentContent = x.CommentContent,
                NewsId = x.NewsId,
                MemberId = x.MemberId,
                Date = x.Date,
                UsersName = x.Members.UsersName,
                NewsHeadline = x.News.Headline,

            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/yorumekle")]
        public InfoModel YorumEkle(CommentModel model)
        {
            if (db.Comment.Count(s => s.MemberId == model.MemberId && s.NewsId == model.NewsId && s.CommentContent == model.CommentContent) > 0)
            {
                info.process = false;
                info.message = "Aynı Kişi, Aynı Haber Aynı Yorumu Yapamaz!";
                return info;
            }
            Comment yeni = new Comment();
            yeni.CommentId = model.CommentId;
            yeni.CommentContent = model.CommentContent;
            yeni.NewsId = model.NewsId;
            yeni.MemberId = model.MemberId;
            yeni.Date = model.Date;

            db.Comment.Add(yeni);
            db.SaveChanges();

            info.process = true;
            info.message = "Yorum Eklendi";

            return info;
        }

        [HttpPut]
        [Route("api/yorumduzenle")]
        public InfoModel YorumDuzenle(CommentModel model)
        {
            Comment kayit = db.Comment.Where(s => s.CommentId == model.CommentId).SingleOrDefault();
            if (kayit == null)
            {
                info.process = false;
                info.message = "Kayıt Bulunamadı!";
                return info;
            }
            kayit.CommentId = model.CommentId;
            kayit.CommentContent = model.CommentContent;
            kayit.NewsId = model.NewsId;
            kayit.MemberId = model.MemberId;
            kayit.Date = model.Date;

            db.SaveChanges();
            info.process = true;
            info.message = "Yorum Düzenlendi";

            return info;
        }

        [HttpDelete]
        [Route("api/yorumsil/{yorumId}")]
        public InfoModel YorumSil(int yorumId)
        {
            Comment kayit = db.Comment.Where(s => s.CommentId == yorumId).SingleOrDefault();
            if (kayit == null)
            {
                info.process = false;
                info.message = "Kayıt Bulunamadı!";
                return info;
            }

            db.Comment.Remove(kayit);
            db.SaveChanges();

            info.process = true;
            info.message = "Yorum Silindi";

            return info;

        }


        #endregion
    }
}
