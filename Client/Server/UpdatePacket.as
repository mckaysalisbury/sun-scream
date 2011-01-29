package Server {
	import com.netease.protobuf.*;
	import flash.utils.IExternalizable;
	import flash.utils.IDataInput;
	import flash.errors.IOError;
	import Server.EntityUpdate;
	import Server.Message;
	// @@protoc_insertion_point(imports)

	// @@protoc_insertion_point(class_metadata)
	public final class UpdatePacket extends com.netease.protobuf.Message implements flash.utils.IExternalizable {
		[ArrayElementType("Server.EntityUpdate")]
		public var Entities:Array = [];

		[ArrayElementType("Server.Message")]
		public var Messages:Array = [];

		private var _ControllingEntityId:int;

		private var _hasControllingEntityId:Boolean = false;

		public function removeControllingEntityId():void {
			_hasControllingEntityId = false;
			_ControllingEntityId = new int();
		}

		public function get hasControllingEntityId():Boolean {
			return _hasControllingEntityId;
		}

		public function set ControllingEntityId(value:int):void {
			_hasControllingEntityId = true;
			_ControllingEntityId = value;
		}

		public function get ControllingEntityId():int {
			return _ControllingEntityId;
		}

		/**
		 *  @private
		 */
		public override function writeToBuffer(output:WritingBuffer):void {
			for (var EntitiesIndex:uint = 0; EntitiesIndex < Entities.length; ++EntitiesIndex) {
				WriteUtils.writeTag(output, WireType.LENGTH_DELIMITED, 1);
				WriteUtils.write_TYPE_MESSAGE(output, Entities[EntitiesIndex]);
			}
			for (var MessagesIndex:uint = 0; MessagesIndex < Messages.length; ++MessagesIndex) {
				WriteUtils.writeTag(output, WireType.LENGTH_DELIMITED, 2);
				WriteUtils.write_TYPE_MESSAGE(output, Messages[MessagesIndex]);
			}
			if (hasControllingEntityId) {
				WriteUtils.writeTag(output, WireType.VARINT, 3);
				WriteUtils.write_TYPE_INT32(output, _ControllingEntityId);
			}
		}

		public function readExternal(input:IDataInput):void {
			var ControllingEntityIdCount:uint = 0;
			while (input.bytesAvailable != 0) {
				var tag:Tag = ReadUtils.readTag(input);
				switch (tag.number) {
				case 1:
					Entities.push(ReadUtils.read_TYPE_MESSAGE(input, new Server.EntityUpdate));
					break;
				case 2:
					Messages.push(ReadUtils.read_TYPE_MESSAGE(input, new Server.Message));
					break;
				case 3:
					if (ControllingEntityIdCount != 0) {
						throw new IOError('Bad data format: UpdatePacket.ControllingEntityId cannot be set twice.');
					}
					++ControllingEntityIdCount;
					ControllingEntityId = ReadUtils.read_TYPE_INT32(input);
					break;
				default:
					ReadUtils.skip(input, tag.wireType);
				}
			}
		}

	}
}
