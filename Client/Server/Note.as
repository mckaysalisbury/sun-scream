package Server {
	import com.netease.protobuf.*;
	import flash.utils.IExternalizable;
	import flash.utils.IDataInput;
	import flash.errors.IOError;
	// @@protoc_insertion_point(imports)

	// @@protoc_insertion_point(class_metadata)
	public final class Note extends com.netease.protobuf.Message implements flash.utils.IExternalizable {
		private var _Target:int;

		private var _hasTarget:Boolean = false;

		public function removeTarget():void {
			_hasTarget = false;
			_Target = new int();
		}

		public function get hasTarget():Boolean {
			return _hasTarget;
		}

		public function set Target(value:int):void {
			_hasTarget = true;
			_Target = value;
		}

		public function get Target():int {
			return _Target;
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

		/**
		 *  @private
		 */
		public override function writeToBuffer(output:WritingBuffer):void {
			if (hasTarget) {
				WriteUtils.writeTag(output, WireType.VARINT, 1);
				WriteUtils.write_TYPE_INT32(output, _Target);
			}
			if (hasType) {
				WriteUtils.writeTag(output, WireType.VARINT, 2);
				WriteUtils.write_TYPE_ENUM(output, _Type);
			}
		}

		public function readExternal(input:IDataInput):void {
			var TargetCount:uint = 0;
			var TypeCount:uint = 0;
			while (input.bytesAvailable != 0) {
				var tag:Tag = ReadUtils.readTag(input);
				switch (tag.number) {
				case 1:
					if (TargetCount != 0) {
						throw new IOError('Bad data format: Note.Target cannot be set twice.');
					}
					++TargetCount;
					Target = ReadUtils.read_TYPE_INT32(input);
					break;
				case 2:
					if (TypeCount != 0) {
						throw new IOError('Bad data format: Note.Type cannot be set twice.');
					}
					++TypeCount;
					Type = ReadUtils.read_TYPE_ENUM(input);
					break;
				default:
					ReadUtils.skip(input, tag.wireType);
				}
			}
		}

	}
}
