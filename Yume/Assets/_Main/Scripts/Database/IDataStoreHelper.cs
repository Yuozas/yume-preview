using System;
using System.Collections.Generic;

public interface IDataStoreHelper
{
    IEnumerable<Type> GetAllTypes();
    IEnumerable<string> GetAllTypeNames();
}