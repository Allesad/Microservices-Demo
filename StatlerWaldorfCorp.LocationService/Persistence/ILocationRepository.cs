using StatlerWaldorfCorp.LocationService.Models;
using System;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.LocationService.Persistence
{
    public interface ILocationRepository
    {
        LocationRecord Add(LocationRecord record);
        LocationRecord Update(LocationRecord record);
        LocationRecord Get(Guid memberId, Guid recordId);
        LocationRecord Delete(Guid memberId, Guid recordId);

        LocationRecord GetLatestForMember(Guid memberId);

        ICollection<LocationRecord> AllForMember(Guid memberId);
    }
}
