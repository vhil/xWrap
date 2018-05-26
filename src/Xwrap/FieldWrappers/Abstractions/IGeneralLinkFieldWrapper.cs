using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xwrap.FieldWrappers.Abstractions
{
    public interface IGeneralLinkFieldWrapper : ILinkFieldWrapper<string>
    {
        Guid ItemId { get; }
        string AlternateText { get; }
        string Description { get; }
        bool IsInternal { get; }
        bool IsMediaLink { get; }
        string Styles { get; }
        string Target { get; }
    }
}
