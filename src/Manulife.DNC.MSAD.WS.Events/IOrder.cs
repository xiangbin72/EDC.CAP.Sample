﻿using System;
using System.Collections.Generic;

namespace Manulife.DNC.MSAD.WS.Events
{
    public interface IOrder
    {
        string ID { get; set; }

        DateTime OrderTime { get; set; }

        List<IOrderItems> OrderItems { get; set; }

        string OrderUserID { get; set; }

        string ProductID { get; set; } // 演示字段，实际应该在OrderItems中

        //string StatusKey { get; set; } // 冗余字段：用于在Event中区分是哪个StatusKey
    }
}
