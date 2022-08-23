#pragma once

#include <ReactCommon/CallInvoker.h>
#include <jsi/jsi.h>

#include <HostObject.h>
#include <include/libindy_vdr.h>

using namespace facebook;

namespace turboModuleUtility {

static const std::string errorPrefix = "Value `";
static const std::string errorInfix = "` is not of type ";

// state of a callback function
struct State {
  jsi::Function cb;
  jsi::Runtime *rt;

  State(jsi::Function *cb_) : cb(std::move(*cb_)) {}
};

// Install the Turbomodule
void registerTurboModule(jsi::Runtime &rt,
                         std::shared_ptr<react::CallInvoker> jsCallInvoker);

// Asserts that a jsi::Value is an object and can be safely transformed
void assertValueIsObject(jsi::Runtime &rt, const jsi::Value *val);

// Converts jsi values to regular cpp values
template <typename T>
T jsiToValue(jsi::Runtime &rt, jsi::Object &options, const char *name,
             bool optional = false);

// Handles an error from within the module and sends it back to the js side
void handleError(jsi::Runtime &rt, ErrorCode code);

// Callback function that makes the host function async
void callback(CallbackId result, ErrorCode code);

// Callback function that makes the host function async with response from rust
// side
void callbackWithResponse(CallbackId result, ErrorCode code,
                          const char *response);

} // namespace turboModuleUtility
