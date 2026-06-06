
import 'package:flutter/material.dart';
import '../../data/datasource/network/service/home_service.dart';

class HomeProvider extends ChangeNotifier {
  BuildContext? context;
  HomeService homeService = HomeService();
  int _count = 0;

  int get count => _count;

  void incrementCount() {
    _count++;
    notifyListeners();
  }
}
