#include <jni.h>
#include <jsi/jsi.h>
#include <fbjni/fbjni.h>
#include <ReactCommon/CallInvokerHolder.h>

#include <turboModuleUtility.h>

using namespace facebook;

struct IndyVdrModule : jni::JavaClass<IndyVdrModule> {
public:
  __unused static constexpr auto kJavaDescriptor = "Lorg/hyperledger/indyvdr/IndyVdrModule;";

  static constexpr auto TAG = "IndyVdr";

  static void registerNatives() {
    javaClassStatic()->registerNatives({ makeNativeMethod("installNative", IndyVdrModule::installNative) });
  }

private:
  static void installNative(jni::alias_ref<jni::JClass>,
                            jlong jsiRuntimePointer,
                            jni::alias_ref<facebook::react::CallInvokerHolder::javaobject> jsCallInvokerHolder) {

    auto runtime = reinterpret_cast<jsi::Runtime*>(jsiRuntimePointer);
    auto jsCallInvoker = jsCallInvokerHolder->cthis()->getCallInvoker();

    turboModuleUtility::registerTurboModule(*runtime, jsCallInvoker);
  }
};

JNIEXPORT jint JNICALL JNI_OnLoad(JavaVM *vm, void *) {
  return facebook::jni::initialize(vm, [] {
    IndyVdrModule::registerNatives();
  });
}
