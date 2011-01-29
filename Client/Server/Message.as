package Server {
	import com.netease.protobuf.*;
	import flash.utils.IExternalizable;
	import flash.utils.IDataInput;
	import flash.errors.IOError;
	// @@protoc_insertion_point(imports)

	// @@protoc_insertion_point(class_metadata)
	public final class Message extends com.netease.protobuf.Message implements flash.utils.IExternalizable {
		private var _Text:String;

		public function removeText():void {
			_Text = null;
		}

		public function get hasText():Boolean {
			return null != _Text;
		}

		public function set Text(value:String):void {
			_Text = value;
		}

		public function get Text():String {
			return _Text;
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
			if (hasText) {
				WriteUtils.writeTag(output, WireType.LENGTH_DELIMITED, 1);
				WriteUtils.write_TYPE_STRING(output, _Text);
			}
			if (hasType) {
				WriteUtils.writeTag(output, WireType.VARINT, 2);
				WriteUtils.write_TYPE_ENUM(output, _Type);
			}
		}

		public function readExternal(input:IDataInput):void {
			var TextCount:uint = 0;
			var TypeCount:uint = 0;
			while (input.bytesAvailable != 0) {
				var tag:Tag = ReadUtils.readTag(input);
				switch (tag.number) {
				case 1:
					if (TextCount != 0) {
						throw new IOError('Bad data format: Message.Text cannot be set twice.');
					}
					++TextCount;
					Text = ReadUtils.read_TYPE_STRING(input);
					break;
				case 2:
					if (TypeCount != 0) {
						throw new IOError('Bad data format: Message.Type cannot be set twice.');
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
