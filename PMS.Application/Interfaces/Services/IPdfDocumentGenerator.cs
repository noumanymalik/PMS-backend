using PMS.Application.Common;

namespace PMS.Application.Interfaces.Services
{
    public interface IPdfDocumentGenerator
    {
        Task<byte[]> Generate<T>(IList<T> source, List<Tuple<string, int>> headers = null, Func<T, object[]> formatterFunc = null);
    }
}