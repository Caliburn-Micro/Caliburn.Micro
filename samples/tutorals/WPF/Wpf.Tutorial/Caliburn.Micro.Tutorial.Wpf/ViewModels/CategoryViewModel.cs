using Caliburn.Micro.Tutorial.Wpf.Models;

namespace Caliburn.Micro.Tutorial.Wpf.ViewModels
  {
  public class CategoryViewModel : Screen
    {
    private BindableCollection<CategoryModel> _categoryList = new BindableCollection<CategoryModel>();
    private CategoryModel _selectedCategoryModel;
    private string _categoryName;
    private string _categoryDescription;

    public BindableCollection<CategoryModel> CategoryList
      {
      get
        {
        return _categoryList;
        }
      set
        {
        _categoryList = value;
        }
      }

    public CategoryModel SelectedCategory
      {
      get
        {
        return _selectedCategoryModel;
        }

      set
        {
        _selectedCategoryModel = value;
        NotifyOfPropertyChange(nameof(SelectedCategory));
        NotifyOfPropertyChange(nameof(CanEdit));
        NotifyOfPropertyChange(nameof(CanDelete));
        }
      }

    public string CategoryName
      {
      get => _categoryName; set
        {
        _categoryName = value;
        NotifyOfPropertyChange(nameof(CategoryName));
        NotifyOfPropertyChange(nameof(CanSave));
        }
      }

    public string CategoryDescription
      {
      get => _categoryDescription; set
        {
        _categoryDescription = value;
        NotifyOfPropertyChange(nameof(CategoryDescription));
        }
      }

    protected override void OnViewLoaded(object view)
      {
      base.OnViewLoaded(view);
      CategoryList.Add(new CategoryModel { CategoryName = "Meals", CategoryDescription = "Lunches and diners" });
      CategoryList.Add(new CategoryModel { CategoryName = "Representation", CategoryDescription = "Gifts for our customers" });
      }


    public bool CanEdit
      {
      get
        {
        return SelectedCategory != null;
        }
      }

    public void Edit()
      {
      CategoryName = SelectedCategory.CategoryName;
      CategoryDescription = SelectedCategory.CategoryDescription;
      }

    public bool CanDelete
      {
      get
        {
        return SelectedCategory != null;
        }
      }

    public void Delete()
      {
      CategoryList.Remove(SelectedCategory);
      Clear();
      }

    public bool CanSave
      {
      get
        {
        return CategoryName?.Length > 2;
        }
      }

    public void Save()
      {
      CategoryModel newCategory = new CategoryModel();
      newCategory.CategoryName = CategoryName;
      newCategory.CategoryDescription = CategoryDescription;
      if (SelectedCategory != null)
        {
        // remove the existing category, needed to update the view
        CategoryList.Remove(SelectedCategory);
        }
      CategoryList.Add(newCategory);
      Clear();
      }

    public void Clear()
      {
      CategoryName = string.Empty;
      CategoryDescription = string.Empty;
      SelectedCategory = null;
      }
    }
  }
