namespace Xwrap.Mvc
{
	public class FormViewModel<TFormData> : ViewModel, IFormViewModel<TFormData> where TFormData : IFormData
	{
		public FormViewModel(TFormData form, IViewModel viewModel)
			: base(viewModel)
		{
			this.Form = form;
		}

		public TFormData Form { get; }
	}
}
