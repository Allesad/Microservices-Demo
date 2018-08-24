using StatlerWaldorfCorp.LocationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatlerWaldorfCorp.LocationService.Persistence
{
    public class InMemoryLocationRepository : ILocationRepository
    {
        private static Dictionary<Guid, SortedList<long, LocationRecord>> _records;

        public InMemoryLocationRepository()
        {
            _records = new Dictionary<Guid, SortedList<long, LocationRecord>>();
        }

        public LocationRecord Add(LocationRecord record)
        {
            var memberRecords = GetMemberRecords(record.MemberId);

            memberRecords.Add(record.Timestamp, record);

            return record;
        }

        public ICollection<LocationRecord> AllForMember(Guid memberId)
        {
            return GetMemberRecords(memberId).Values;
        }

        public LocationRecord Delete(Guid memberId, Guid recordId)
        {
            var memberRecords = GetMemberRecords(memberId);
            var record = memberRecords.Values.FirstOrDefault(r => r.Id == recordId);

            if (record != null)
            {
                memberRecords.Remove(record.Timestamp);
            }

            return record;

        }

        public LocationRecord Get(Guid memberId, Guid recordId)
        {
            return GetMemberRecords(memberId).Values.FirstOrDefault(r => r.Id == recordId);
        }

        public LocationRecord GetLatestForMember(Guid memberId)
        {
            return GetMemberRecords(memberId).Values.LastOrDefault();
        }

        public LocationRecord Update(LocationRecord record)
        {
            return Delete(record.MemberId, record.Id);
        }

        private SortedList<long, LocationRecord> GetMemberRecords(Guid memberId)
        {
            if (!_records.ContainsKey(memberId))
            {
                _records.Add(memberId, new SortedList<long, LocationRecord>());
            }

            return _records[memberId];
        }
    }
}
