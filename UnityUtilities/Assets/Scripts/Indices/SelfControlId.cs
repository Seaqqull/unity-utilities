using System.Collections.Generic;

namespace UnityUtilities.Identifiers
{
    public class SelfControlId
    {
        class Range
        {
            uint _left, // Included
                 _right, // Excluded
                 _size;


            public bool IsEmpty
            {
                get { return this._size == 0; }
            }
            public uint Size
            {
                get { return this._size; }
            }
            public uint Right
            {
                get { return this._right; }
            }
            public uint Left
            {
                get { return this._left; }
            }


            public Range(uint left, uint count)
            {
                if (uint.MaxValue - count < left) // To prevent "overflow" exception create empty range
                {
                    _left = 0;
                    _right = 0;
                    _size = 0;
                }
                else
                {
                    _left = left;
                    _right = left + count;
                    _size = count;
                }
            }


            public bool Add()
            {
                return (AddLeft()) ? true : AddRight();
            }

            public bool AddLeft()
            {
                if (_left == uint.MinValue) return false;

                _left--;
                _size++;
                return true;
            }

            public bool AddRight()
            {
                if (_right == uint.MaxValue) return false;

                _right++;
                _size++;
                return true;
            }

            public bool RemoveLeft()
            {
                if (_size == 0) return false;

                _left++;
                _size--;
                return true;
            }

            public bool RemoveRight()
            {
                if (_size == 0) return false;

                _right--;
                _size--;
                return true;
            }

            public bool Add(uint number)
            {
                if (CanAddLeft(number))
                    AddLeft();
                else if (CanAddRight(number))
                    AddRight();
                else
                    return false;

                return true;
            }

            public bool Unite(Range range)
            {
                if ((this._right != range._left) &&
                    (range._right != this._left)) return false;

                this._left = (this._left > range._left) ? range._left : this._left;
                this._right = (this._right < range._right) ? range._right : this._right;
                this._size = this._size + range._size;

                return true;
            }

            public bool CanAdd(uint number)
            {
                return (CanAddLeft(number) || CanAddRight(number));
            }

            public bool IsLeft(uint number)
            {
                return (_size == 0) ? false : number == _left;
            }

            public bool IsRight(uint number)
            {
                return (_size == 0) ? false : (number == (_right - 1));
            }

            public bool InRange(uint number)
            {
                return (_size == 0) ? false : ((number >= _left) && (number < _right));
            }

            public bool CanAddLeft(uint number)
            {
                return (_left == uint.MinValue) ? false : ((_left - 1) == number);
            }

            public bool CanAddRight(uint number)
            {
                return (_right == uint.MaxValue) ? false : (_right == number);
            }

            public bool GetNumber(ref uint number)
            {
                if (_size == 0) return false;

                number = _left++;
                _size--;
                return true;
            }


            public override string ToString()
            {
                string str = "";

                str += "{";
                str += (_left + ", " + _right);
                str += "}:" + _size;

                return str;
            }

        }


        private List<Range> _ranges;
        private uint _capacity; // Max allowable count of identifiers that can be provided for use
        private uint _avaible; // Count of identifiers that can be provided for use        

        public uint Capacity
        {
            get { return this._capacity; }
        }
        public uint Avaible
        {
            get { return this._avaible; }
        }


        public SelfControlId(uint count = uint.MaxValue)
        {
            _capacity = count;
            _avaible = count;

            _ranges = new List<Range>();

            AddRange(0, _capacity);
        }


        private int AddRange(Range range)
        {
            for (int i = 0; i < _ranges.Count; i++)
            {
                if (_ranges[i].Size >= range.Size)
                {
                    _ranges.Insert(i, range);
                    return i;
                }
            }

            _ranges.Add(range);
            return (_ranges.Count - 1);
        }

        /// <summary>
        /// Attempts to unite given range with another
        /// </summary>
        /// <param name="index">Index of the given range</param>
        private void Defragment(int index)
        {
            for (int i = 0; i < _ranges.Count; i++)
            {
                if (i == index) continue;

                if (_ranges[i].Unite(_ranges[index])) // unite two ranges
                {
                    _ranges.RemoveAt(index); // delete the one that was added
                    break;
                }
            }
        }

        private int AddRange(uint left, uint count)
        {
            return AddRange(new Range(left, count));
        }


        public bool Extend(uint size)
        {
            if (uint.MaxValue - size < _capacity) return false;

            int insertedIndex = AddRange(_capacity, size);
            _capacity += size;
            _avaible += size;

            Defragment(insertedIndex);

            return true;
        }

        public bool ReturnIndex(uint number)
        {
            for (int i = 0; i < _ranges.Count; i++)
                if (_ranges[i].InRange(number)) // checking if this number was already added
                    return false;

            for (int i = 0; i < _ranges.Count; i++)
            {
                if (_ranges[i].Add(number)) // if index was successfully added to some range
                {
                    Defragment(i);

                    _avaible++;
                    return true;
                }
            }

            AddRange(number, 1);
            _avaible++;
            return true;
        }

        public bool GetIndex(ref uint number)
        {
            if (_avaible == 0) return false;

            int index = _ranges.Count - 1;

            for (int i = (index - 1); i > -1; i--)
            {
                if (_ranges[i].Size < _ranges[index].Size) // Finding the smallest range
                    index = i;
            }

            _ranges[index].GetNumber(ref number);
            _avaible--;

            if (_ranges[index].IsEmpty) _ranges.RemoveAt(index);

            return true;
        }

        public bool Shrink(uint size, bool asManyAsPossible = false)
        {
            if ((asManyAsPossible && (_avaible == 0)) ||
                (!asManyAsPossible && (size > _avaible))) return false;

            for (int i = 0; i < _ranges.Count; i++)
            {
                if (_ranges[i].InRange(_capacity - 1)) // Finding the actual end of the global range
                {
                    if (!asManyAsPossible && (size > _ranges[i].Size))
                    {
                        return false;
                    }
                    else if (_ranges[i].Size == size)
                    {
                        _ranges.RemoveAt(i);
                        _capacity -= size;
                        _avaible -= size;
                        return true;
                    }

                    uint cnter = 0;

                    for (uint j = _ranges[i].Size - 1; cnter < size; j--)
                    {
                        if (_ranges[i].RemoveRight())
                            cnter++;
                        if (j == 0) break;
                    }

                    if (_ranges[i].IsEmpty)
                        _ranges.RemoveAt(i);

                    _capacity -= cnter;
                    _avaible -= cnter;

                    return true;
                }
            }
            return false;
        }


        public override string ToString()
        {
            string str = "(" + _capacity + ", " + _avaible + ")->";

            for (int i = 0; i < _ranges.Count; i++)
            {
                str += _ranges[i];
                if (i != _ranges.Count - 1) str += ",";
            }

            return str;
        }
    }
}
