package Server {
	import com.netease.protobuf.*;
	import flash.utils.IExternalizable;
	import flash.utils.IDataInput;
	import flash.errors.IOError;
	// @@protoc_insertion_point(imports)

	// @@protoc_insertion_point(class_metadata)
	public final class EntityUpdate extends com.netease.protobuf.Message implements flash.utils.IExternalizable {
		private var _Id:int;

		private var _hasId:Boolean = false;

		public function removeId():void {
			_hasId = false;
			_Id = new int();
		}

		public function get hasId():Boolean {
			return _hasId;
		}

		public function set Id(value:int):void {
			_hasId = true;
			_Id = value;
		}

		public function get Id():int {
			return _Id;
		}

		private var _Name:String;

		public function removeName():void {
			_Name = null;
		}

		public function get hasName():Boolean {
			return null != _Name;
		}

		public function set Name(value:String):void {
			_Name = value;
		}

		public function get Name():String {
			return _Name;
		}

		private var _LocationX:int;

		private var _hasLocationX:Boolean = false;

		public function removeLocationX():void {
			_hasLocationX = false;
			_LocationX = new int();
		}

		public function get hasLocationX():Boolean {
			return _hasLocationX;
		}

		public function set LocationX(value:int):void {
			_hasLocationX = true;
			_LocationX = value;
		}

		public function get LocationX():int {
			return _LocationX;
		}

		private var _LocationY:int;

		private var _hasLocationY:Boolean = false;

		public function removeLocationY():void {
			_hasLocationY = false;
			_LocationY = new int();
		}

		public function get hasLocationY():Boolean {
			return _hasLocationY;
		}

		public function set LocationY(value:int):void {
			_hasLocationY = true;
			_LocationY = value;
		}

		public function get LocationY():int {
			return _LocationY;
		}

		private var _Type:int;

		private var _hasType:Boolean = false;

		public function removeType():void {
			_hasType = false;
			_Type = new int();
		}

		public function get hasType():Boolean {
			return _hasType;
		}

		public function set Type(value:int):void {
			_hasType = true;
			_Type = value;
		}

		public function get Type():int {
			return _Type;
		}

		private var _Rotation:int;

		private var _hasRotation:Boolean = false;

		public function removeRotation():void {
			_hasRotation = false;
			_Rotation = new int();
		}

		public function get hasRotation():Boolean {
			return _hasRotation;
		}

		public function set Rotation(value:int):void {
			_hasRotation = true;
			_Rotation = value;
		}

		public function get Rotation():int {
			return _Rotation;
		}

		[ArrayElementType("int")]
		public var Towed:Array = [];

		private var _Size:int;

		private var _hasSize:Boolean = false;

		public function removeSize():void {
			_hasSize = false;
			_Size = new int();
		}

		public function get hasSize():Boolean {
			return _hasSize;
		}

		public function set Size(value:int):void {
			_hasSize = true;
			_Size = value;
		}

		public function get Size():int {
			return _Size;
		}

		/**
		 *  @private
		 */
		public override function writeToBuffer(output:WritingBuffer):void {
			if (hasId) {
				WriteUtils.writeTag(output, WireType.VARINT, 1);
				WriteUtils.write_TYPE_INT32(output, _Id);
			}
			if (hasName) {
				WriteUtils.writeTag(output, WireType.LENGTH_DELIMITED, 2);
				WriteUtils.write_TYPE_STRING(output, _Name);
			}
			if (hasLocationX) {
				WriteUtils.writeTag(output, WireType.VARINT, 3);
				WriteUtils.write_TYPE_INT32(output, _LocationX);
			}
			if (hasLocationY) {
				WriteUtils.writeTag(output, WireType.VARINT, 4);
				WriteUtils.write_TYPE_INT32(output, _LocationY);
			}
			if (hasType) {
				WriteUtils.writeTag(output, WireType.VARINT, 5);
				WriteUtils.write_TYPE_ENUM(output, _Type);
			}
			if (hasRotation) {
				WriteUtils.writeTag(output, WireType.VARINT, 6);
				WriteUtils.write_TYPE_INT32(output, _Rotation);
			}
			for (var TowedIndex:uint = 0; TowedIndex < Towed.length; ++TowedIndex) {
				WriteUtils.writeTag(output, WireType.VARINT, 7);
				WriteUtils.write_TYPE_INT32(output, Towed[TowedIndex]);
			}
			if (hasSize) {
				WriteUtils.writeTag(output, WireType.VARINT, 8);
				WriteUtils.write_TYPE_INT32(output, _Size);
			}
		}

		public function readExternal(input:IDataInput):void {
			var IdCount:uint = 0;
			var NameCount:uint = 0;
			var LocationXCount:uint = 0;
			var LocationYCount:uint = 0;
			var TypeCount:uint = 0;
			var RotationCount:uint = 0;
			var SizeCount:uint = 0;
			while (input.bytesAvailable != 0) {
				var tag:Tag = ReadUtils.readTag(input);
				switch (tag.number) {
				case 1:
					if (IdCount != 0) {
						throw new IOError('Bad data format: EntityUpdate.Id cannot be set twice.');
					}
					++IdCount;
					Id = ReadUtils.read_TYPE_INT32(input);
					break;
				case 2:
					if (NameCount != 0) {
						throw new IOError('Bad data format: EntityUpdate.Name cannot be set twice.');
					}
					++NameCount;
					Name = ReadUtils.read_TYPE_STRING(input);
					break;
				case 3:
					if (LocationXCount != 0) {
						throw new IOError('Bad data format: EntityUpdate.LocationX cannot be set twice.');
					}
					++LocationXCount;
					LocationX = ReadUtils.read_TYPE_INT32(input);
					break;
				case 4:
					if (LocationYCount != 0) {
						throw new IOError('Bad data format: EntityUpdate.LocationY cannot be set twice.');
					}
					++LocationYCount;
					LocationY = ReadUtils.read_TYPE_INT32(input);
					break;
				case 5:
					if (TypeCount != 0) {
						throw new IOError('Bad data format: EntityUpdate.Type cannot be set twice.');
					}
					++TypeCount;
					Type = ReadUtils.read_TYPE_ENUM(input);
					break;
				case 6:
					if (RotationCount != 0) {
						throw new IOError('Bad data format: EntityUpdate.Rotation cannot be set twice.');
					}
					++RotationCount;
					Rotation = ReadUtils.read_TYPE_INT32(input);
					break;
				case 7:
					if (tag.wireType == WireType.LENGTH_DELIMITED) {
						ReadUtils.readPackedRepeated(input, ReadUtils.read_TYPE_INT32, Towed);
						break;
					}
					Towed.push(ReadUtils.read_TYPE_INT32(input));
					break;
				case 8:
					if (SizeCount != 0) {
						throw new IOError('Bad data format: EntityUpdate.Size cannot be set twice.');
					}
					++SizeCount;
					Size = ReadUtils.read_TYPE_INT32(input);
					break;
				default:
					ReadUtils.skip(input, tag.wireType);
				}
			}
		}

	}
}
