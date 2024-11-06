using System;
using System.Collections.Generic;
using Humanizer;
using src.Domain.AvailabilitySlotAggregate;
using src.Domain.Shared;

namespace src.Domain.AvailabilitySlotAggregate{

    public class AvailabilitySlot: Entity<StaffID>
    {
        public StaffID StaffID { get; set; }
        public Dictionary<int, Slot> Slots { get; set; }

        public AvailabilitySlot()
        {
            Slots = new Dictionary<int, Slot>();
        }

        public bool addAvailabilitySlot(int day , int startTime, int endTime)
        {

            try{
                Slot slot = new Slot( startTime, endTime);

                if (Slots.ContainsKey(day))
                {
                    Slots[day] = slot;
                }
                else
                {
                    Slots.Add(day, slot);
                }
            }catch(Exception e){
                return false;
            }

            return true;
        }

        public bool removeAvailability(int day){
            if (!Slots.ContainsKey(day))
            {
                return false;
            }
            else
            {
                Slots.Remove(day);
                return true;
            }
        }
    }
}