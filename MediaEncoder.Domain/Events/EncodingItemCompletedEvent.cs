﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaEncoder.Domain.Events
{
    public record EncodingItemCompletedEvent(Guid Id, string SourceSystem, Uri OutputUrl) : INotification;
}
