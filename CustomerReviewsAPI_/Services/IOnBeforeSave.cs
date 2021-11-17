

namespace CustomerReviewsAPI_.Controllers
{
    public interface IOnBeforeSave
    {
        public void BeforeSave(Type type);
    }
}
