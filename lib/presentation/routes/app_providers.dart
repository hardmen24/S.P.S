import '../providers/home_provider.dart';

import 'package:provider/provider.dart';
import 'package:provider/single_child_widget.dart';

class AppProviders {
  static List<SingleChildWidget> getProviders() {
    return [

      // Provider list
          ChangeNotifierProvider(create: (_) => HomeProvider()),
    ];
  }
}
