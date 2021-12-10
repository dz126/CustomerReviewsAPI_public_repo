

using System;

namespace CustomerReviewsAPI_.Controllers
{
    // Before saving the instance to the database ,update the properties
    public interface IOnBeforeSave
    {
        public void BeforeSave(Type type);
    }
}
