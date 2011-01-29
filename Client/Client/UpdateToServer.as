package Client {
	import com.netease.protobuf.*;
	import flash.utils.IExternalizable;
	import flash.utils.IDataInput;
	import flash.errors.IOError;
	import Client.ThrustUpdate;
	// @@protoc_insertion_point(imports)

	// @@protoc_insertion_point(class_metadata)
	public final class UpdateToServer extends com.netease.protobuf.Message implements flash.utils.IExternalizable {
		private var _Thrust:Client.ThrustUpdate;

		public function removeThrust():void {
			_Thrust = null;
		}

		public function get hasThrust():Boolean {
			return null != _Thrust;
		}

		public function set Thrust(value:Client.ThrustUpdate):void {
			_Thrust = value;
		}

		public function get Thrust():Client.ThrustUpdate {
			return _Thrust;
		}

		/**
		 *  @private
		 */
		public override function writeToBuffer(output:WritingBuffer):void {
			if (hasThrust) {
				WriteUtils.writeTag(output, WireType.LENGTH_DELIMITED, 1);
				WriteUtils.write_TYPE_MESSAGE(output, _Thrust);
			}
		}

		public function readExternal(input:IDataInput):void {
			var ThrustCount:uint = 0;
			while (input.bytesAvailable != 0) {
				var tag:Tag = ReadUtils.readTag(input);
				switch (tag.number) {
				case 1:
					if (ThrustCount != 0) {
						throw new IOError('Bad data format: UpdateToServer.Thrust cannot be set twice.');
					}
					++ThrustCount;
					Thrust = new Client.ThrustUpdate;
					ReadUtils.read_TYPE_MESSAGE(input, Thrust);
					break;
				default:
					ReadUtils.skip(input, tag.wireType);
				}
			}
		}

	}
}
