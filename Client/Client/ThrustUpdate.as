package Client {
	import com.netease.protobuf.*;
	import flash.utils.IExternalizable;
	import flash.utils.IDataInput;
	import flash.errors.IOError;
	// @@protoc_insertion_point(imports)

	// @@protoc_insertion_point(class_metadata)
	public final class ThrustUpdate extends com.netease.protobuf.Message implements flash.utils.IExternalizable {
		public var Angle:int;

		public var Distance:int;

		/**
		 *  @private
		 */
		public override function writeToBuffer(output:WritingBuffer):void {
			WriteUtils.writeTag(output, WireType.VARINT, 1);
			WriteUtils.write_TYPE_INT32(output, Angle);
			WriteUtils.writeTag(output, WireType.VARINT, 2);
			WriteUtils.write_TYPE_INT32(output, Distance);
		}

		public function readExternal(input:IDataInput):void {
			var AngleCount:uint = 0;
			var DistanceCount:uint = 0;
			while (input.bytesAvailable != 0) {
				var tag:Tag = ReadUtils.readTag(input);
				switch (tag.number) {
				case 1:
					if (AngleCount != 0) {
						throw new IOError('Bad data format: ThrustUpdate.Angle cannot be set twice.');
					}
					++AngleCount;
					Angle = ReadUtils.read_TYPE_INT32(input);
					break;
				case 2:
					if (DistanceCount != 0) {
						throw new IOError('Bad data format: ThrustUpdate.Distance cannot be set twice.');
					}
					++DistanceCount;
					Distance = ReadUtils.read_TYPE_INT32(input);
					break;
				default:
					ReadUtils.skip(input, tag.wireType);
				}
			}
			if (AngleCount != 1) {
				throw new IOError('Bad data format: ThrustUpdate.Angle must be set.');
			}
			if (DistanceCount != 1) {
				throw new IOError('Bad data format: ThrustUpdate.Distance must be set.');
			}
		}

	}
}
