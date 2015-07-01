﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customers.Domain.ViewHelper
{
    public static class ListProviders
    {
        public static IListProvider Current { get; private set; }

        static ListProviders()
        {
            Current = new DefaultListProvider();
        }

        public static void SetListProvider(Func<IListProvider> providerAccessor)
        {
            Current = providerAccessor();
        }
    }
}
