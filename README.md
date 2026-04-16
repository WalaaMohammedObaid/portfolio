# 🚀 Portfolio App — ASP.NET Core MVC

تطبيق Portfolio كامل مبني بـ ASP.NET Core MVC مع SQLite.

---

## 📋 المتطلبات

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)

---

## ⚡ طريقة التشغيل

### 1. استعادة الحزم
```bash
dotnet restore
```

### 2. تشغيل المشروع
```bash
dotnet run
```

### 3. افتح المتصفح على
```
https://localhost:5001
```

> قاعدة البيانات `portfolio.db` ستُنشأ تلقائياً عند أول تشغيل.

---

## 🔐 بيانات الدخول للأدمن

| الحقل | القيمة |
|-------|--------|
| اسم المستخدم | `admin` |
| كلمة المرور | `admin123` |

> ⚠️ لتغيير كلمة المرور: افتح `Controllers/AccountController.cs` وعدّل:
> ```csharp
> private const string AdminUsername = "admin";
> private const string AdminPassword = "admin123";
> ```

---

## 🗂 هيكل المشروع

```
PortfolioApp/
├── Controllers/
│   ├── HomeController.cs       ← الصفحة الرئيسية العامة
│   ├── AdminController.cs      ← لوحة الإدارة (CRUD)
│   └── AccountController.cs    ← تسجيل الدخول والخروج
├── Models/
│   ├── Project.cs              ← نموذج المشروع
│   └── LoginViewModel.cs       ← نموذج تسجيل الدخول
├── Data/
│   └── AppDbContext.cs         ← قاعدة البيانات (EF Core + SQLite)
├── Views/
│   ├── Home/Index.cshtml       ← صفحة Portfolio العامة
│   ├── Admin/
│   │   ├── Index.cshtml        ← قائمة المشاريع في الأدمن
│   │   ├── Create.cshtml       ← إضافة مشروع
│   │   └── Edit.cshtml         ← تعديل مشروع
│   ├── Account/Login.cshtml    ← صفحة تسجيل الدخول
│   └── Shared/_Layout.cshtml   ← القالب الرئيسي
├── wwwroot/
│   ├── css/site.css            ← التصميم
│   └── uploads/                ← صور المشاريع المرفوعة
└── Program.cs                  ← إعداد التطبيق
```

---

## ✨ المميزات

- ✅ عرض المشاريع بتصميم Grid أنيق
- ✅ إضافة / تعديل / حذف المشاريع
- ✅ رفع صور المشاريع
- ✅ نظام تسجيل دخول للأدمن
- ✅ قاعدة بيانات SQLite (لا تحتاج إعداد)
- ✅ تصميم Dark Mode بالكامل
- ✅ دعم اللغة العربية (RTL)
- ✅ متجاوب مع الجوال
