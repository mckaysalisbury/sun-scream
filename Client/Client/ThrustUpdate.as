package Client {
	import com.netease.protobuf.*;
	import flash.utils.IExternalizable;
	import flash.utils.IDataInput;
	import flash.errors.IOError;
	// @@protoc_insertion_point(imports)

	// @@protoc_insertion_point(class_metadata)
	public final class ThrustUpdate extends com.netease.protobuf.Message implements flash.utils.IExternalizable {
		public var RelativeX:int;

		public var RelativeY:int;

		/**
		 *  @private
		 */
		public override function writeToBuffer(output:WritingBuffer):void {
			WriteUtils.writeTag(output, WireType.VARINT, 1);
			WriteUtils.write_TYPE_INT32(output, RelativeX);
			WriteUtils.writeTag(output, WireType.VARINT, 2);
			WriteUtils.write_TYPE_INT32(output, RelativeY);
		}

		public function readExternal(input:IDataInput):void {
			var RelativeXCount:uint = 0;
			var RelativeYCount:uint = 0;
			while (input.bytesAvailable != 0) {
				var tag:Tag = ReadUtils.readTag(input);
				switch (tag.number) {
				case 1:
					if (RelativeXCount != 0) {
						throw new IOError('Bad data format: ThrustUpdate.RelativeX cannot be set twice.');
					}
					++RelativeXCount;
					RelativeX = ReadUtils.read_TYPE_INT32(input);
					break;
				case 2:
					if (RelativeYCount != 0) {
						throw new IOError('Bad data format: ThrustUpdate.RelativeY cannot be set twice.');
					}
					++RelativeYCount;
					RelativeY = ReadUtils.read_TYPE_INT32(input);
					break;
				default:
					ReadUtils.skip(input, tag.wireType);
				}
			}
			if (RelativeXCount != 1) {
				throw new IOError('Bad data format: ThrustUpdate.RelativeX must be set.');
			}
			if (RelativeYCount != 1) {
				throw new IOError('Bad data format: ThrustUpdate.RelativeY must be set.');
			}
		}

	}
}
