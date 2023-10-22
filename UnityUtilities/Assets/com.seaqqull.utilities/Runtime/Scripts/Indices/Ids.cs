using UnityEngine;
using Utilities.Attributes;


namespace Utilities.Identifiers
{
    public enum DataState
    {
        Unknown,
        Updated,
        Transferred,
        Processed,
        Changed,
        Deleted
    }


    class ExtendedId<TClass> : GlobalId<TClass>
    {
#pragma warning disable 0649
        [SerializeField] private uint _additionalId;
#pragma warning restore 0649

        public uint AdditionalId
        {
            get { return this._additionalId; }
        }
    }

    class GlobalId<TClass>
    {
        protected static uint _idCounter = 0;

        [ReadOnly] [SerializeField] protected uint _id;

        protected DataState _state = DataState.Unknown;        

        public DataState State
        {
            get { return this._state; }
        }
        public uint Id
        {
            get { return this._id; }
        }


        /// <summary>
        /// Used to assign id for each entity with this class.
        /// </summary>
        public void CalculateId()
        {
            if (_state == DataState.Unknown)
            {
                _id = _idCounter++;
                _state = DataState.Updated;
            }
        }
    }
}